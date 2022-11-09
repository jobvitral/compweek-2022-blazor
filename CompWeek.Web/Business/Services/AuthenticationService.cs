using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Text;
using Blazored.LocalStorage;
using CompWeek.Domain.Commons;
using CompWeek.Domain.ViewModels;
using CompWeek.Web.Domain.Interfaces.Services;
using CompWeek.Web.Domain.Models;
using Newtonsoft.Json;

namespace CompWeek.Web.Business.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorageService;
    public AuthenticationService(HttpClient httpClient, ILocalStorageService localStorageService)
    {
        _httpClient = httpClient;
        _localStorageService = localStorageService;
    }

    public async Task<AuthenticationResponse?> Authenticate(AuthenticationRequest request)
    {
        var url = "v1/authentication";

        var json = JsonConvert.SerializeObject(request);
        var response = await _httpClient.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<AuthenticationResponse>();

            if (result != null)
            {
                await WriteToStorage(result);
            }

            return result;
        }

        var exception = await response.Content.ReadFromJsonAsync<CustomException>();
        throw new CustomException(exception!.Errors);
    }

    public async Task<Credential?> GetCurrent()
    {
        var authentication = await ReadFromStorage();
        
        if (authentication != null)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(authentication.Token);

            if (jwt?.Payload != null)
            {
                var result = new Credential
                {
                    Id = int.Parse(jwt.Payload["id"].ToString()!),
                    RoleId = int.Parse(jwt.Payload["role"].ToString()!),
                    Name = jwt.Payload["name"].ToString(),
                    Email = jwt.Payload["email"].ToString(),
                    Scope = authentication.Scope
                };

                return result;
            }
        }

        return null;
    }

    public async Task WriteToStorage(AuthenticationResponse? item)
    {
        if (item != null)
        {
            await _localStorageService.SetItemAsync("authentication", item);
        }
    }

    public async Task ClearStorage()
    {
        await _localStorageService.RemoveItemAsync("authentication");
    }
    
    public async Task<AuthenticationResponse?> ReadFromStorage()
    {
        return await _localStorageService.GetItemAsync<AuthenticationResponse>("authentication");
    }
}