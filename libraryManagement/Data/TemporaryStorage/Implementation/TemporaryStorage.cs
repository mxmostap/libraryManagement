namespace libraryManagement.Data.TemporaryStorage.Implementation;

public class TemporaryStorage<T>: ITemporaryStorage<T> where T : class
{
    private readonly List<T> _storage;
    private int _idCounter;

    public TemporaryStorage(List<T> storage)
    {
        _storage = storage;
        _idCounter = storage.Count();
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await Task.FromResult(_storage);
    }
    
    public async Task<T?> GetByIdAsync(int id)
    {
        return await Task.FromResult(_storage.FirstOrDefault(item => GetId(item) == id));
    }

    public async Task<T> AddAsync(T item)
    {
        SetId(item, ++_idCounter);
        _storage.Add(item);
        return await Task.FromResult(item);
    }

    public async Task<T?> UpdateAsync(T item)
    {
        var id = GetId(item);
        var existingItem = _storage.FirstOrDefault(t => GetId(t) == id);
        if (existingItem == null)
            return null;
        
        var index = _storage.IndexOf(existingItem);
        if (index >= 0)
        {
            _storage[index] = item;
            return await Task.FromResult(item);
        }
        return null;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var item = _storage.FirstOrDefault(t => GetId(t) == id);
        if (item != null)
            return await Task.FromResult(_storage.Remove(item));
        return false;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await Task.FromResult(_storage.Any(t => GetId(t) == id));
    }
    
    private int GetId(T item)
    {
        var property = typeof(T).GetProperty("Id");
        return (int)property.GetValue(item);
    }
    
    private void SetId(T item, int id)
    {
        var property = typeof(T).GetProperty("Id");
        property.SetValue(item, id);
    }
}