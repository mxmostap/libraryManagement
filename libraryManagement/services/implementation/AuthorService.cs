using libraryManagement.models.entities;
using libraryManagement.repositories;

namespace libraryManagement.services.implementation;

public class AuthorService: IAuthorService
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IBookRepository _bookRepository;

    public AuthorService(IAuthorRepository authorRepository, IBookRepository bookRepository)
    {
        _authorRepository = authorRepository;
        _bookRepository = bookRepository;
    }

    public async Task<List<Author>> GetAllAuthorsAsync()
        => await _authorRepository.GetAllAuthorsAsync();

    public async Task<Author?> GetAuthorByIdAsync(int id)
        => await _authorRepository.GetAuthorByIdAsync(id);

    public async Task<Author> AddAuthorAsync(Author author)
    {
        foreach (var bookId in author.BooksId)
        {
            if (!await _bookRepository.BookExistsAsync(bookId))
                throw new ArgumentException("Книга с указанным ID не существует!");
        }

        return await _authorRepository.AddAuthorAsync(author);
    }

    public async Task<Author?> UpdateAuthorAsync(Author author)
    {
        var existingAuthor = await _authorRepository.GetAuthorByIdAsync(author.Id);
        if (existingAuthor != null)
        {
            foreach (var bookId in author.BooksId)
            {
                if (!await _bookRepository.BookExistsAsync(bookId))
                    throw new ArgumentException("Книга с указанным ID не существует!");
            }

            return await _authorRepository.UpdateAuthorAsync(author);
        }

        return null;
    }

    public async Task<bool> DeleteAuthorAsync(int id)
    {
        var author = await _authorRepository.GetAuthorByIdAsync(id);
        if (author != null)
            return await _authorRepository.DeleteAuthorAsync(id);
        
        return false;
    }
}