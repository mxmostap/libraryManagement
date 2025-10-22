using libraryManagement.models;
using libraryManagement.services;
using Microsoft.AspNetCore.Mvc;

namespace libraryManagement.controllers;

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
            return NotFound($"Книга с Id {id} не найдена!");

        return Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBookAsync([FromBody] Book book)
    {
        if (book == null)
            return BadRequest("Данные книги не могут быть пустыми!");

        if (string.IsNullOrWhiteSpace(book.Title))
            return BadRequest("Название книги не может быть пустым!");

        if (book.PublishedYear <= 0 || book.PublishedYear > DateTime.Now.Year)
            return BadRequest("Год публикации введен некорректно!");
        try
        {
            var createdBook = await _bookService.AddBookAsync(book);
            return CreatedAtRoute("GetBook", new {id = createdBook.Id}, createdBook);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBookAsync(int id, [FromBody] Book book)
    {
        if (book == null)
            return BadRequest("Данные книги не могут быть пустыми!");

        if (id != book.Id)
            return BadRequest("Ошибка ввода Id");

        if (string.IsNullOrWhiteSpace(book.Title))
            return BadRequest("Название книги не может быть пустым!");
        
        if (book.PublishedYear <= 0 || book.PublishedYear > DateTime.Now.Year)
            return BadRequest("Год публикации введен некорректно!");

        try
        {
            var updatedBook = await _bookService.UpdateBookAsync(book);
            if (updatedBook == null)
                return NotFound($"Книга с Id {id} не найдена");
            
            return Ok(updatedBook);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBookAsync(int id)
    {
        var result = await _bookService.DeleteBookAsync(id);
        if (!result)
            return NotFound($"Книга с Id {id} не найдена.");

        return NoContent();
    }
}