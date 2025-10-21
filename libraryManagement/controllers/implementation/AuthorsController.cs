using libraryManagement.models;
using libraryManagement.services;
using Microsoft.AspNetCore.Mvc;

namespace libraryManagement.controllers.implementation;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController: ControllerBase, IAuthorsController
{
    private readonly IAuthorsService _authorsService;

    public AuthorsController(IAuthorsService authorsService)
    {
        _authorsService = authorsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAuthorsAsync()
    {
        var authors = await _authorsService.GetAllAuthorsAsync();
        return Ok(authors);
    }

    [HttpGet("{id}", Name = "GetAuthor")]
    public async Task<IActionResult> GetAuthorByIdAsync(int id)
    {
        var author = await _authorsService.GetAuthorByIdAsync(id);
        if (author == null)
            return NotFound($"Автор с Id {id} не найден.");

        return Ok(author);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAuthorAsync([FromBody] Author author)
    {
        if (author == null)
            return BadRequest("Данные автора не могут быть пустыми!");
        
        if (string.IsNullOrWhiteSpace(author.Name))
            return BadRequest("Имя автора не может быть пустым!");

        var createdAuthor = await _authorsService.AddAuthorAsync(author);
        
        return CreatedAtRoute("GetAuthor", new {id = createdAuthor.Id }, createdAuthor);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAuthorAsync(int id, [FromBody] Author author)
    {
        if (author == null)
            return BadRequest("Данные автора не могут быть пустыми!");

        if (id != author.Id)
            return BadRequest("Ошибка ввода Id!");
        
        if (string.IsNullOrWhiteSpace(author.Name))
            return BadRequest("Имя автора не может быть пустым!");

        var updatedAuthor = await _authorsService.UpdateAuthorAsync(author);
        if (updatedAuthor == null)
            return NotFound($"Автор с Id {id} не найден.");

        return Ok(updatedAuthor);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthorAsync(int id)
    {
        var result = await _authorsService.DeleteAuthorAsync(id);
        if (!result)
            return NotFound($"Автор с Id {id} не найден.");

        return NoContent();
    }
}