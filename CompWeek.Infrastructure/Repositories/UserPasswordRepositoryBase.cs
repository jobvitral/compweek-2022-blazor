using CompWeek.Domain.Commons;
using CompWeek.Domain.Filters;
using CompWeek.Domain.Interfaces.Repositories;
using CompWeek.Domain.Models;
using CompWeek.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CompWeek.Infrastructure.Repositories;

public class UserPasswordRepositoryBase : IUserPasswordRepositoryBase
{
    private readonly MysqlContext _context;
    
    public UserPasswordRepositoryBase(MysqlContext context)
    {
        _context = context;
    }
    
    public async Task<UserPassword?> Get(int param)
    {
        var result = await _context.UserPassword!
            .FirstOrDefaultAsync(a => a.Id == param);

        return result;
    }

    public async Task<List<UserPassword>> Get(UserPasswordFilter filter)
    {
        var query = _context.UserPassword!
            .AsQueryable();
        
        query = Filter(query, filter);
        
        var result = await query.ToListAsync();

        return result;
    }
    
    public async Task<List<UserPassword>> GetByUser(int userId)
    {
        var result = await _context.UserPassword!
            .Where(a => a.UserId == userId)
            .ToListAsync();

        return result;
    }

    public async Task<UserPassword?> GetCurrent(int userId)
    {
        var result = await _context.UserPassword!
            .FirstAsync(a => a.UserId == userId && a.IsActive == true);

        return result;
    }

    public IQueryable<UserPassword> Filter(IQueryable<UserPassword> query, UserPasswordFilter? filter)
    {
        if (filter != null)
        {
            if (filter.Ids != null)
                query = query.Where(a => filter.Ids.Contains(a.Id));

            if (filter.Users != null)
                query = query.Where(a => filter.Users.Contains((int)a.UserId!));
        
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
            
            if (filter.InactivationPeriod != null)
            {
                if (filter.InactivationPeriod.Item1 != null)
                    query = query.Where(a => a.InactivatedAt >= filter.InactivationPeriod.Item1);
            
                if (filter.InactivationPeriod.Item2 != null)
                    query = query.Where(a => a.InactivatedAt <= filter.InactivationPeriod.Item2);
            }

            if (filter.IsActive != null)
                query = query.Where(a => a.IsActive == filter.IsActive);
        }

        return query;
    }

    public async Task<UserPassword?> Insert(UserPassword entity)
    {
        entity.ClearDependencies();
        
        // cryptografa a senha
        entity.Password = CryptographyHelper.Encrypt(entity.Password!);
        entity.IsActive = true;

        // salva item
        _context.UserPassword!.Add(entity);
        await _context.SaveChangesAsync();
        
        // desabilita as outras senhas
        var otherPasswords = await _context.UserPassword
            .Where(a => a.UserId == entity.UserId &&
                        a.IsActive == true &&
                        a.Id != entity.Id)
            .ToListAsync();

        foreach (var othenPassword in otherPasswords)
        {
            othenPassword.IsActive = false;
            othenPassword.InactivatedAt = DateHelper.GetNow();

            _context.UserPassword!.Update(othenPassword);
            await _context.SaveChangesAsync();
        }

        return await Get(entity.Id);
    }

    public async Task<UserPassword?> Update(UserPassword entity)
    {
        entity.ClearDependencies();
        
        _context.UserPassword!.Update(entity);
        _context.Entry(entity).Property(a => a.UserId).IsModified = false;
        
        await _context.SaveChangesAsync();

        if (entity.IsActive)
        {
            // desabilita as outras senhas
            var otherPasswords = await _context.UserPassword
                .Where(a => a.UserId == entity.Id &&
                            a.IsActive == true &&
                            a.Id != entity.Id)
                .ToListAsync();

            foreach (var othenPassword in otherPasswords)
            {
                othenPassword.IsActive = false;
                othenPassword.InactivatedAt = DateHelper.GetNow();

                _context.UserPassword!.Update(othenPassword);
                await _context.SaveChangesAsync();
            }

        }

        return await Get(entity.Id);
    }
    
    public async Task<UserPassword?> Update(int userId, string newPassword)
    {
        var entity = await GetCurrent(userId);

        if (entity != null)
        {
            entity.Password = CryptographyHelper.Encrypt(newPassword);
            
            _context.UserPassword!.Update(entity);
            _context.Entry(entity).Property(a => a.UserId).IsModified = false;

            await _context.SaveChangesAsync();
        }

        return entity;
    }

    public async Task<UserPassword?> Delete(int param)
    {
        var item = await Get(param);

        if (item != null)
        {
            _context.UserPassword!.Remove(item);
            await _context.SaveChangesAsync();
        }

        return item;
    }
}