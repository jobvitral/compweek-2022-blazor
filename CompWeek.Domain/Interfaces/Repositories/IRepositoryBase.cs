namespace CompWeek.Domain.Interfaces.Repositories;

public interface IRepositoryBase<T, I, F>
{
    Task<T?> Get(I param);
    Task<List<T>> Get(F filter);
    IQueryable<T> Filter(IQueryable<T> query, F? filter);
    Task<T?> Insert(T entity);
    Task<T?> Update(T entity);
    Task<T?> Delete(I param);
}
