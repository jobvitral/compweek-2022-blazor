using CompWeek.Identity.Domain.Interfaces;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Stores;

namespace CompWeek.Identity.Configuration;

public class ClientStore : IClientStore
{
    private readonly IUnitOfWork _unitOfWork;

    public ClientStore(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Client> FindClientByIdAsync(string clientId)
    {
        var user = await _unitOfWork.UserRepository.Get(int.Parse(clientId));

        return await Task.FromResult(new Client
        {
            ClientName = user!.Name,
            ClientId = clientId,
            AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
            RequireClientSecret = false,
            AllowedScopes = new List<string> { user!.Role!.Scope },
            AccessTokenLifetime = 86400
        });
    }
}