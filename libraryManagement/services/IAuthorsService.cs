using libraryManagement.models;

namespace libraryManagement.services;

public interface IAuthorsService
{
    Task<List<Author>> GetAllAuthorsAsync();
    Task<Author?> GetAuthorByIdAsync(int id);
    Task<Author> AddAuthorAsync(Author author);
    Task<Author?> UpdateAuthorAsync(Author author);
    Task<bool> DeleteAuthorAsync(int id);
    Task<bool> AuthorExistsAsync(int id);
    
    //List<Book> GetAuthorBooks(int authorId);
}