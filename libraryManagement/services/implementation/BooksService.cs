using libraryManagement.models;

namespace libraryManagement.services.implementation;

public class BooksService: IBooksService
{
    private readonly List<Book> _books;
    private readonly IAuthorsService _authorsService;
    private int _idCounter = 1;

    public BooksService(IAuthorsService authorsService)
    {
        _authorsService = authorsService;
        
        _books = new List<Book>()
        {
            new Book
            {
                Id = _idCounter++,
                Title = "Письмо к женщине",
                PublishedYear = 2012,
                AuthorId = 2
            },
            new Book
            {
                Id = _idCounter++,
                Title = "Клен ты мой опавший",
                PublishedYear = 2017,
                AuthorId = 2
            },
            new Book
            {
                Id = _idCounter++,
                Title = "Евгений Онегин",
                PublishedYear = 2015,
                AuthorId = 1
            },
        };
        /*foreach (var book in _books)
        {
            var author = _authorsService.GetAuthorById(book.AuthorId);
            if (author != null)
            {
                book.Author = author;
            }
        }*/
    }

    public async Task<List<Book>> GetAllBooksAsync()
    {
        return await Task.FromResult(_books);
    }

    public async Task<Book?> GetBookByIdAsync(int id)
    {
        return await Task.FromResult(_books.FirstOrDefault(b => b.Id == id));
    }

    public async Task<Book> AddBookAsync(Book book)
    {
        if (!await _authorsService.AuthorExistsAsync(book.AuthorId))
            throw new ArgumentException("Автор с указанным ID не существует!");
        
        book.Id = _idCounter++;
        _books.Add(book);
        
        return await Task.FromResult(book);
    }
    
    public async Task<Book?> UpdateBookAsync(Book book)
    {
        var existingBook = await GetBookByIdAsync(book.Id);
        if (existingBook != null)
        {
            if (!await _authorsService.AuthorExistsAsync(book.AuthorId))
                throw new ArgumentException("Автор с указанным ID не существует!");
                
            existingBook.Title = book.Title;
            existingBook.PublishedYear = book.PublishedYear;
            existingBook.AuthorId = book.AuthorId;

            return existingBook;
        }

        return null;
    }

    public async Task<bool> DeleteBookAsync(int id)
    {
        var book = await GetBookByIdAsync(id);
        if (book != null)
            return await Task.FromResult(_books.Remove(book));
        
        return false;
    }
}