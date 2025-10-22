using libraryManagement.models;

namespace libraryManagement.services.implementation;

public class AuthorService: IAuthorService
{
    private readonly List<Author> _authors;
    private int _idCounter = 1;

    public AuthorService()
    {
        _authors = new List<Author>()
        {
            new Author
            {
                Id = _idCounter++,
                Name = "Александр Пушкин",
                DateOfBirth = new DateTime(1799, 6, 6)
            },
            new Author
            {
                Id = _idCounter++,
                Name = "Сергей Есенин",
                DateOfBirth = new DateTime(1895, 10, 3)
            }
        };
    }

    public async Task<List<Author>> GetAllAuthorsAsync()
    {
        return await Task.FromResult(_authors);
    }

    public async Task<Author?> GetAuthorByIdAsync(int id)
    {
        return await Task.FromResult(
            _authors.FirstOrDefault(a => a.Id == id));
    }

    public async Task<Author> AddAuthorAsync(Author author)
    {
        author.Id = _idCounter++;
        _authors.Add(author);
        return await Task.FromResult(author);
    }

    public async Task<Author?> UpdateAuthorAsync(Author author)
    {
        var existingAuthor = await GetAuthorByIdAsync(author.Id);
        if (existingAuthor != null)
        {
            existingAuthor.Name = author.Name;
            existingAuthor.DateOfBirth = author.DateOfBirth;
            return existingAuthor;
        }

        return null;
    }

    public async Task<bool> DeleteAuthorAsync(int id)
    {
        var author = await GetAuthorByIdAsync(id);
        if (author != null)
            return await Task.FromResult(_authors.Remove(author));
        
        return false;
    }
    
    public async Task<bool> AuthorExistsAsync(int id)
    {
        return await Task.FromResult(_authors.Any(a => a.Id == id));
    }
}