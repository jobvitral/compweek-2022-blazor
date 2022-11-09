using CompWeek.Domain.Commons;
using CompWeek.Domain.Filters;
using CompWeek.Domain.Interfaces.Repositories;
using CompWeek.Domain.Models;
using CompWeek.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CompWeek.Infrastructure.Repositories;

public class UserRequestRepositoryBase : IUserRequestRepositoryBase
{
    private readonly MysqlContext _context;
    
    public UserRequestRepositoryBase(MysqlContext context)
    {
        _context = context;
    }
    
    public async Task<UserRequest?> Get(int param)
    {
        var entity = await _context.UserRequest!
            .Include(a => a.User)
            .FirstOrDefaultAsync(a => a.Id == param);

        return entity;
    }
    
    public async Task<UserRequest?> GetByKey(string key)
    {
        var entity = await _context.UserRequest!
            .Include(a => a.User)
            .FirstOrDefaultAsync(a => a.ValidationKey == key);

        return entity;
    }

    public async Task<List<UserRequest>> Get(UserRequestFilter filter)
    {
        var query = _context.UserRequest!.AsQueryable();
        query = Filter(query, filter);

        return await query.ToListAsync();
    }

    public IQueryable<UserRequest> Filter(IQueryable<UserRequest> query, UserRequestFilter? filter)
    {
        if (filter != null)
        {
            if (filter.Ids != null)
                query = query.Where(a => filter.Ids.Contains(a.Id));

            if (filter.Users != null)
                query = query.Where(a => filter.Users.Contains(a.UserId!.Value));

            if (filter.Keys != null)
                query = query.Where(a => filter.Keys.Contains(a.ValidationKey));

            if (filter.CreationPeriod != null)
            {
                if (filter.CreationPeriod.Item1 != null)
                    query = query.Where(a => a.CreatedAt >= filter.CreationPeriod.Item1);

                if (filter.CreationPeriod.Item2 != null)
                    query = query.Where(a => a.CreatedAt <= filter.CreationPeriod.Item2);
            }

            if (filter.ExpirationPeriod != null)
            {
                if (filter.ExpirationPeriod.Item1 != null)
                    query = query.Where(a => a.ExpiresAt >= filter.ExpirationPeriod.Item1);

                if (filter.ExpirationPeriod.Item2 != null)
                    query = query.Where(a => a.ExpiresAt <= filter.ExpirationPeriod.Item2);
            }

            if (filter.UsedPeriod != null)
            {
                if (filter.UsedPeriod.Item1 != null)
                    query = query.Where(a => a.ValidatedAt >= filter.UsedPeriod.Item1);

                if (filter.UsedPeriod.Item2 != null)
                    query = query.Where(a => a.ValidatedAt <= filter.UsedPeriod.Item2);
            }

            if (filter.IsExpired != null)
            {
                if (filter.IsExpired == true)
                    query = query.Where(a => a.ExpiresAt < DateHelper.GetNow());
                else
                    query = query.Where(a => a.ExpiresAt >= DateHelper.GetNow());
            }

            if (filter.IsUsed != null)
                if (filter.IsUsed == true)
                    query = query.Where(a => a.ValidatedAt != null);
                else
                    query = query.Where(a => a.ExpiresAt == null);
        }
        
        return query;
    }

    public async Task<UserRequest?> Insert(UserRequest entity)
    {
        entity.ClearDependencies();
        
        // salva o item
        _context.UserRequest!.Add(entity);
        await _context.SaveChangesAsync();

        return await Get(entity.Id);
    }

    public async Task<UserRequest?> Update(UserRequest entity)
    {
        entity.ClearDependencies();
        
        // salva o item
        _context.UserRequest!.Update(entity);
        await _context.SaveChangesAsync();

        return await Get(entity.Id);
    }

    public async Task<UserRequest?> Delete(int param)
    {
        var item = await Get(param);

        if (item != null)
        {
            _context.UserRequest!.Remove(item);
            await _context.SaveChangesAsync();
        }

        return item;
    }

    
}