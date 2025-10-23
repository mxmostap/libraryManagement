using libraryManagement.extensions.mapping;
using libraryManagement.Models.DTOs;
using libraryManagement.Models.Exceptions;
using libraryManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace libraryManagement.Controllers;

[ApiController]
[Route("api/books")]
public class BooksController: ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBooksAsync()
    {
        var books = await _bookService.GetAllBooksAsync();
        return Ok(books);
    }

    [HttpGet("{id}", Name = "GetBook")]
    public async Task<IActionResult> GetBookByIdAsync(int id)
    {
        var book = await _bookService.GetBookByIdAsync(id);
        if (book == null)
            throw new NotFoundException($"Книга с Id {id} не найдена!");

        return Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBookAsync([FromBody] CreateBookDto book)
    {
        if (book == null)
            throw new BadRequestException("Данные книги не могут быть пустыми!");

        if (string.IsNullOrWhiteSpace(book.Title))
            throw new BadRequestException("Название книги не может быть пустым!");

        if (book.PublishedYear <= 0 || book.PublishedYear > DateTime.Now.Year)
            throw new BadRequestException("Год публикации введен некорректно!");
        
        var createdBook = await _bookService.AddBookAsync(book.ToEntity());
        
        return CreatedAtRoute("GetBook", new {id = createdBook.Id}, createdBook);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBookAsync(int id, [FromBody] CreateBookDto book)
    {
        if (book == null)
            throw new BadRequestException("Данные книги не могут быть пустыми!");

        if (string.IsNullOrWhiteSpace(book.Title))
            throw new BadRequestException("Название книги не может быть пустым!");
        
        if (book.PublishedYear <= 0 || book.PublishedYear > DateTime.Now.Year)
            throw new BadRequestException("Год публикации введен некорректно!");

       
        var updatedBook = await _bookService.UpdateBookAsync(book.ToEntity(id));
        if (updatedBook == null)
            throw new NotFoundException($"Книга с Id {id} не найдена");
            
        return Ok(updatedBook);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBookAsync(int id)
    {
        var result = await _bookService.DeleteBookAsync(id);
        if (!result)
            throw new NotFoundException($"Книга с Id {id} не найдена.");

        return NoContent();
    }
}