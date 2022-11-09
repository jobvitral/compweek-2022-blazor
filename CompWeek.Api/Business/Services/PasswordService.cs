using CompWeek.Api.Domain.Interfaces;
using CompWeek.Api.Domain.Interfaces.Services;
using CompWeek.Api.Domain.Models;
using CompWeek.Domain.Commons;
using CompWeek.Domain.Errors;
using CompWeek.Domain.Filters;
using CompWeek.Domain.Models;
using CompWeek.Domain.ViewModels;
using Microsoft.Extensions.Options;
using SendGrid;

namespace CompWeek.Api.Business.Services;

public class PasswordService : IPasswordService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOptions<AppSettings> _appSettings;
    
    public PasswordService(IUnitOfWork unitOfWork, IOptions<AppSettings> appSettings)
    {
        _unitOfWork = unitOfWork;
        _appSettings = appSettings;
    }
    
    public async Task<User?> Update(PasswordUpdateRequest request)
    {
        var validation = request.Validate();

        if (validation.IsValid)
        {
            // verify currentUser is equals the request user
            var login = await _unitOfWork.UserRepository.GetByLogin(request.DocumentNumber!, request.CurrentPassword!);

            if (login == null)
                throw new CustomException(UpdatePasswordError.LoginIsNull);

            await _unitOfWork.UserPasswordRepository.Update(login.Id, request.NewPassword!);
            
            return login;
        }

        throw new CustomException(validation.Errors);
    }

    public async Task<UserRequest?> RequestRecovery(PasswordRecoverRequest item)
    {
        var requestValidation = item.Validate();

        if (requestValidation.IsValid)
        {
            var users = await _unitOfWork.UserRepository.Get(new UserFilter { Document = item.DocumentNumber });

            if (users.Any())
            {
                var user = users.FirstOrDefault();

                if (user != null)
                {
                    var userRequest = new UserRequest { UserId = user.Id };
                    var userRequestValidation = userRequest.ValidateInsert();

                    if (userRequestValidation.IsValid)
                    {
                        var result = await _unitOfWork.UserRequestRepository.Insert(userRequest);
                        //result!.User!.Requests = null;
                        
                        // envia o email
                        var emailResult = await SendMail(result);

                        if (emailResult!.IsSuccessStatusCode)
                        {
                            // remove campos nao necessarios
                            result!.ValidationKey = null;

                            return result;
                        }

                        throw new CustomException("Erro ai enviar o email de recuperação");
                    }

                    throw new CustomException(userRequestValidation.Errors);
                }
            }

            throw new CustomException(UserError.EmailIsNull);
        }

        throw new CustomException(requestValidation.Errors);
    }

    public async Task<Response?> SendMail(UserRequest? item)
    {
        var subject = "Recuperação de senha do ViraVenda";
        var message = $"Olá {item!.User!.Name}. Sua chave de recuperação de senha é {item.ValidationKey}";

        var sendgrid = new SendgridHelper
        {
            ApiKey = _appSettings.Value.Sendgrid!.ApiKey,
            FromEmail = _appSettings.Value.Sendgrid.FromEmail,
            FromName = _appSettings.Value.Sendgrid.FromName,
            Subject = subject,
            ToEmail = item.User.Email,
            ToName = item.User.Name,
            PlainTextContent = message,
            HtmlContent = message
        };

        var result = await sendgrid.SendEmail();

        return result;
    }

    public async Task<UserRequest?> Recover(PasswordRecoverUpdate item)
    {
        var requestValidation = item.Validate();

        if (requestValidation.IsValid)
        {
            var users = await _unitOfWork.UserRepository.Get(new UserFilter { Document = item.DocumentNumber });

            if (users.Any())
            {
                var user = users.FirstOrDefault();
                var userRequest = await _unitOfWork.UserRequestRepository.GetByKey(item.ValidationKey!);

                if (userRequest != null)
                {
                    var userRequestValidation = userRequest.ValidateUse(user);

                    if (userRequestValidation.IsValid)
                    {
                        var password = new UserPassword
                        {
                            UserId = user!.Id,
                            Password = item.Password!,
                            CreatedAt = DateHelper.GetNow()
                        };
                        
                        // update password
                        await _unitOfWork.UserPasswordRepository.Insert(password);

                        // update used date
                        userRequest.ValidatedAt = DateHelper.GetNow();

                        var result = await _unitOfWork.UserRequestRepository.Update(userRequest);
                        
                        return result;
                    }

                    throw new CustomException(userRequestValidation.Errors);
                }

                throw new CustomException(UserRequestError.RequestIsNull);
            }

            throw new CustomException(UserError.EmailIsNull);
        }

        throw new CustomException(requestValidation.Errors);
    }
}