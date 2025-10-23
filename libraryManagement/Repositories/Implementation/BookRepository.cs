using libraryManagement.Data.TemporaryStorage;
using libraryManagement.Models.Entities;

namespace libraryManagement.Repositories.Implementation;

public class BookRepository: IBookRepository
{
    private readonly ITemporaryStorage<Book> _storage;

    public BookRepository(ITemporaryStorage<Book> storage)
    {
        _storage = storage;
    }

    public async Task<List<Book>> GetAllBooksAsync()
    {
        var authors =await  _storage.GetAllAsync();
        return authors;
    }

    public async Task<Book?> GetBookByIdAsync(int id)
        => await _storage.GetByIdAsync(id);

    public async Task<Book> AddBookAsync(Book author)
        => await _storage.AddAsync(author);
    
    public async Task<Book?> UpdateBookAsync(Book author)
        => await _storage.UpdateAsync(author);
    
    public async Task<bool> DeleteBookAsync(int id)
        => await _storage.DeleteAsync(id);

    public async Task<bool> BookExistsAsync(int id) 
        => await _storage.ExistsAsync(id);
}