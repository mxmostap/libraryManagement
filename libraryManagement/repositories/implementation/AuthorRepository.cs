using libraryManagement.models.entities;
using libraryManagement.storage;

namespace libraryManagement.repositories.implementation;

public class AuthorRepository: IAuthorRepository
{
    private readonly ITemporaryStorage<Author> _storage;

    public AuthorRepository(ITemporaryStorage<Author> storage)
    {
        _storage = storage;
    }

    public async Task<List<Author>> GetAllAuthorsAsync()
    {
        var authors =await  _storage.GetAllAsync();
        return authors;
    }

    public async Task<Author?> GetAuthorByIdAsync(int id)
        => await _storage.GetByIdAsync(id);

    public async Task<Author> AddAuthorAsync(Author author)
        => await _storage.AddAsync(author);
    
    public async Task<Author?> UpdateAuthorAsync(Author author)
        => await _storage.UpdateAsync(author);
    
    public async Task<bool> DeleteAuthorAsync(int id)
        => await _storage.DeleteAsync(id);

    public async Task<bool> AuthorExistsAsync(int id)
        => await _storage.ExistsAsync(id);
}