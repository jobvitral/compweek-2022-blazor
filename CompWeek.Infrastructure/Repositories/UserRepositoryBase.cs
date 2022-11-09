using CompWeek.Domain.Commons;
using CompWeek.Domain.Filters;
using CompWeek.Domain.Interfaces.Repositories;
using CompWeek.Domain.Models;
using CompWeek.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CompWeek.Infrastructure.Repositories;

public class UserRepositoryBase : IUserRepositoryBase
{
    private readonly MysqlContext _context;
    
    public UserRepositoryBase(MysqlContext context)
    {
        _context = context;
    }
    
    public async Task<User?> Get(int param)
    {
        var result = await _context.User!
            .Include(a => a.Role)
            .FirstOrDefaultAsync(a => a.Id == param);

        return result;
    }

    public async Task<List<User>> Get(UserFilter filter)
    {
        var query = _context.User!
            .Include(a => a.Role)
            .AsQueryable();
        
        query = Filter(query, filter);
        
        var result = await query.ToListAsync();

        return result;
    }

    public IQueryable<User> Filter(IQueryable<User> query, UserFilter? filter)
    {
        if (filter != null)
        {
            if (filter.Ids != null)
                query = query.Where(a => filter.Ids.Contains(a.Id));

            if (filter.Roles != null)
                query = query.Where(a => filter.Roles.Contains((int)a.RoleId!));
        
            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(a => a.Name!.Contains(filter.Name));

            if (!string.IsNullOrEmpty(filter.Document))
            {
                if(filter.Document.Contains("@"))
                    query = query.Where(a => a.Email == filter.Document);
                else
                    query = query.Where(a => a.DocumentNumber == filter.Document);
            }

            if (filter.RegistrationPeriod != null)
            {
                if (filter.RegistrationPeriod.Item1 != null)
                    query = query.Where(a => a.CreatedAt >= filter.RegistrationPeriod.Item1);
            
                if (filter.RegistrationPeriod.Item2 != null)
                    query = query.Where(a => a.CreatedAt <= filter.RegistrationPeriod.Item2);
            }
        
            if (filter.ConfirmationPeriod != null)
            {
                if (filter.ConfirmationPeriod.Item1 != null)
                    query = query.Where(a => a.ConfirmedAt >= filter.ConfirmationPeriod.Item1);
            
                if (filter.ConfirmationPeriod.Item2 != null)
                    query = query.Where(a => a.ConfirmedAt <= filter.ConfirmationPeriod.Item2);
            }

            if (filter.IsConfirmed != null)
                query = query.Where(a => a.IsConfirmed == filter.IsConfirmed);
        
            if (filter.IsActive != null)
                query = query.Where(a => a.IsActive == filter.IsActive);
        }

        return query;
    }

    public async Task<User?> Insert(User entity)
    {
        entity.ClearDependencies();
        
        // salva item
        _context.User!.Add(entity);
        await _context.SaveChangesAsync();

        return await Get(entity.Id);
    }

    public async Task<User?> Update(User entity)
    {
        entity.ClearDependencies();
        
        _context.User!.Update(entity);
        _context.Entry(entity).Property(a => a.RoleId).IsModified = false;
        
        await _context.SaveChangesAsync();

        return await Get(entity.Id);
    }

    public async Task<User?> Delete(int param)
    {
        var item = await Get(param);

        if (item != null)
        {
            _context.User!.Remove(item);
            await _context.SaveChangesAsync();
        }

        return item;
    }

    public async Task<User?> GetByLogin(string document, string password)
    {
        var encryptedPassword = CryptographyHelper.Encrypt(password);
        var query = _context.User!
            .Include(a => a.Role)
            .Include(a => a.Passwords)
            .AsQueryable();
        
        query = document.Contains("@") ? 
            query.Where(a => a.Email == document) : 
            query.Where(a => a.DocumentNumber == document);

        query = query.Where(a => a.Passwords!.Any(b => 
            b.Password == encryptedPassword && 
            b.IsActive == true));

        var result = await query.FirstOrDefaultAsync();

        if (result != null)
        {
            result.Passwords = null;
        }

        return result;
    }

    public async Task<bool> CheckEmail(string email, int? userId)
    {
        var query = _context.User!.AsQueryable();
        query = query.Where(a => a.Email == email);

        if (userId != null)
            query = query.Where(a => a.Id != userId);

        return await query.AnyAsync();
    }

    public async Task<bool> CheckDocument(string document, int? userId)
    {
        var query = _context.User!.AsQueryable();
        query = query.Where(a => a.DocumentNumber == document);

        if (userId != null)
            query = query.Where(a => a.Id != userId);

        return await query.AnyAsync();
    }
}