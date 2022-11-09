using CompWeek.Api.Domain.Interfaces;
using CompWeek.Api.Domain.Interfaces.Services;
using CompWeek.Domain.Commons;
using CompWeek.Domain.Filters;
using CompWeek.Domain.Models;

namespace CompWeek.Api.Business.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUnitOfService _unitOfService;

    public UserService(IUnitOfWork unitOfWork, IUnitOfService unitOfService)
    {
        _unitOfWork = unitOfWork;
        _unitOfService = unitOfService;
    }


    public async Task<User?> Get(int param)
    {
        var result = await _unitOfWork.UserRepository.Get(param);

        return result;
    }

    public async Task<List<User>> Get(UserFilter filter)
    {
        var result = await _unitOfWork.UserRepository.Get(filter);

        return result;
    }

    public async Task<User?> Insert(User entity)
    {
        var emailCheck = await _unitOfWork.UserRepository.CheckEmail(entity.Email!, null);
        var documentCheck = await _unitOfWork.UserRepository.CheckDocument(entity.DocumentNumber!, null);
        var userValidation = entity.ValidateInsert(emailCheck, documentCheck);

        if (userValidation.IsValid)
        {
            var password = entity.Passwords!.FirstOrDefault();
            var passwordValidation = password!.Validate(false);

            if (passwordValidation.IsValid)
            {
                var result = await _unitOfWork.UserRepository.Insert(entity);
                
                // grava a senha
                password.UserId = result!.Id;
                await _unitOfWork.UserPasswordRepository.Insert(password);

                return result;   
            }
        }

        throw new CustomException(userValidation.Errors);
    }

    public async Task<User?> Update(User entity)
    {
        var emailCheck = await _unitOfWork.UserRepository.CheckEmail(entity.Email!, entity.Id);
        var documentCheck = await _unitOfWork.UserRepository.CheckDocument(entity.DocumentNumber!, entity.Id);
        var validation = entity.ValidateUpdate(emailCheck, documentCheck);

        if (validation.IsValid)
        {
            var result = await _unitOfWork.UserRepository.Update(entity);

            return result;
        }

        throw new CustomException(validation.Errors);

    }

    public async Task<User?> Delete(int param)
    {
        var result = await _unitOfWork.UserRepository.Delete(param);

        return result;
    }
}