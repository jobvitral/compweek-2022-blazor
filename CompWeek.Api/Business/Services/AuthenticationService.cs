using CompWeek.Api.Domain.Interfaces;
using CompWeek.Api.Domain.Interfaces.Services;
using CompWeek.Api.Domain.Models;
using CompWeek.Domain.Commons;
using CompWeek.Domain.Errors;
using CompWeek.Domain.ViewModels;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Options;

namespace CompWeek.Api.Business.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly AppSettings _appSettings;
    private readonly IActionContextAccessor _actionContextAccessor;
    
    public AuthenticationService(IUnitOfWork unitOfWork, IOptions<AppSettings> appSettings, IActionContextAccessor actionContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _appSettings = appSettings.Value;
        _actionContextAccessor = actionContextAccessor;
    }
    
    public async Task<AuthenticationResponse?> Authenticate(AuthenticationRequest request)
    {
        var validation = request.Validate();

        if(validation.IsValid)
        {
            var user = await _unitOfWork.UserRepository.GetByLogin(request.DocumentNumber!, request.Password!);

            if(user != null)
            {
                HttpClient httpClient = new HttpClient();

                var tokenClient = new TokenClient(httpClient, new TokenClientOptions
                {
                    Address = $"{_appSettings.IdentityServer}/connect/token",
                    ClientId = user.Id.ToString()
                });

                var tokenResponse = await tokenClient.RequestPasswordTokenAsync(user.DocumentNumber, request.Password);

                if (tokenResponse.Error != null)
                {
                    throw new CustomException(tokenResponse.Error);
                }

                var authenticationResponse = new AuthenticationResponse(user, tokenResponse.AccessToken, tokenResponse.ExpiresIn);

                return authenticationResponse;
            }

            throw new CustomException(AuthenticationError.UserIsNull);
        }

        throw new CustomException(validation.Errors);
    }
}