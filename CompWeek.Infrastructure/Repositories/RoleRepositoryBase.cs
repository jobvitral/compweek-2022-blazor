using CompWeek.Domain.Filters;
using CompWeek.Domain.Interfaces.Repositories;
using CompWeek.Domain.Models;
using CompWeek.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CompWeek.Infrastructure.Repositories;

public class RoleRepositoryBase : IRoleRepositoryBase
{
    private readonly MysqlContext _context;
    
    public RoleRepositoryBase(MysqlContext context)
    {
        _context = context;
    }
    
    public async Task<Role?> Get(int param)
    {
        var result = await _context.Role!
            .FirstOrDefaultAsync(a => a.Id == param);

        return result;
    }

    public async Task<List<Role>> Get(RoleFilter filter)
    {
        var query = _context.Role!
            .AsQueryable();

        query = Filter(query, filter);

        return await query.ToListAsync();
    }

    public IQueryable<Role> Filter(IQueryable<Role> query, RoleFilter? filter)
    {
        if (filter != null)
        {
            if (filter.Ids != null)
                query = query.Where(a => filter.Ids.Contains(a.Id));

            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(a => a.Name!.Equals(filter.Name));
            
            if(!string.IsNullOrEmpty(filter.Scope))
                query = query.Where(a => a.Scope!.Equals(filter.Scope));

            if (filter.IsDefault != null)
                query = query.Where(a => a.IsDefault == filter.IsDefault);
        }

        return query;
    }

    public async Task<Role?> Insert(Role entity)
    {
        entity.ClearDependencies();
        
        _context.Role!.Add(entity);
        await _context.SaveChangesAsync();

        return await Get(entity.Id);
    }

    public async Task<Role?> Update(Role entity)
    {
        entity.ClearDependencies();
        
        _context.Role!.Update(entity);
        await _context.SaveChangesAsync();

        return await Get(entity.Id);
    }

    public async Task<Role?> Delete(int param)
    {
        var item = await Get(param);

        if (item != null)
        {
            _context.Role!.Remove(item);
            await _context.SaveChangesAsync();
        }

        return item;
    }
}