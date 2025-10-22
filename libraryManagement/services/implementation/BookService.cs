using libraryManagement.models.entities;
using libraryManagement.repositories;


namespace libraryManagement.services.implementation;

public class BookService: IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorRepository _authorRepository;

    public BookService(IBookRepository bookRepository, IAuthorRepository authorService)
    {
        _bookRepository = bookRepository;
        _authorRepository = authorService;
    }

    public async Task<List<Book>> GetAllBooksAsync() 
        => await _bookRepository.GetAllBooksAsync();

    public async Task<Book?> GetBookByIdAsync(int id)
        => await _bookRepository.GetBookByIdAsync(id);
    

    public async Task<Book> AddBookAsync(Book book)
    {
        if (!await _authorRepository.AuthorExistsAsync(book.AuthorId))
            throw new ArgumentException("Автор с указанным ID не существует!");

        return await _bookRepository.AddBookAsync(book);
    }
    
    public async Task<Book?> UpdateBookAsync(Book book)
    {
        var existingBook = await _bookRepository.GetBookByIdAsync(book.Id);
        if (existingBook != null)
        {
            if (!await _authorRepository.AuthorExistsAsync(book.AuthorId))
                throw new ArgumentException("Автор с указанным ID не существует!");

            return await _bookRepository.UpdateBookAsync(book);
        }

        return null;
    }

    public async Task<bool> DeleteBookAsync(int id)
    {
        var book = await _bookRepository.GetBookByIdAsync(id);
        if (book != null)
            return await _bookRepository.DeleteBookAsync(id);
        
        return false;
    }
}