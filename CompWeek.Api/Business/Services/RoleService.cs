using CompWeek.Api.Domain.Interfaces;
using CompWeek.Api.Domain.Interfaces.Services;
using CompWeek.Domain.Commons;
using CompWeek.Domain.Filters;
using CompWeek.Domain.Models;

namespace CompWeek.Api.Business.Services;

public class RoleService : IRoleService
{
    private readonly IUnitOfWork _unitOfWork;
    
    public RoleService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Role?> Get(int param)
    {
        return await _unitOfWork.RoleRepository.Get(param);
    }

    public async Task<List<Role>> Get(RoleFilter filter)
    {
        return await _unitOfWork.RoleRepository.Get(filter);
    }

    public async Task<Role?> Insert(Role entity)
    {
        var validation = entity.Validate();

        if (validation.IsValid)
        {
            return await _unitOfWork.RoleRepository.Insert(entity);
        }

        throw new CustomException(validation.Errors);
    }

    public async Task<Role?> Update(Role entity)
    {
        var validation = entity.Validate();

        if (validation.IsValid)
        {
            return await _unitOfWork.RoleRepository.Update(entity);
        }

        throw new CustomException(validation.Errors);
    }

    public async Task<Role?> Delete(int param)
    {
        return await _unitOfWork.RoleRepository.Delete(param);
    }
}