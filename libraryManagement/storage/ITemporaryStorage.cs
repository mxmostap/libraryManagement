namespace libraryManagement.storage;

public interface ITemporaryStorage<T> where T : class
{
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<T> AddAsync(T item);
    Task<T?> UpdateAsync(T item);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}