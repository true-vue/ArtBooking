namespace ArtBooking.Storage;

public interface IRepositoryBase<T> where T : class
{
    Task<IQueryable<T>> GetMultipleAsync();
    Task<T?> GetAsync(int id);
    Task<T> AddAsync(T item);
    Task<T> UpdateAsync(T item);
    Task DeleteAsync(int id);
}