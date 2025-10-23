using libraryManagement.Models.Entities;

namespace libraryManagement.Services;

public interface IAuthorService
{
    Task<List<Author>> GetAllAuthorsAsync();
    Task<Author?> GetAuthorByIdAsync(int id);
    Task<Author> AddAuthorAsync(Author author);
    Task<Author?> UpdateAuthorAsync(Author author);
    Task<bool> DeleteAuthorAsync(int id);
}