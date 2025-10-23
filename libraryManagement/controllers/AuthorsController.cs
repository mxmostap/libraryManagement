using libraryManagement.extensions.mapping;
using libraryManagement.models;
using libraryManagement.models.DTOs;
using libraryManagement.services;
using Microsoft.AspNetCore.Mvc;

namespace libraryManagement.controllers;

[ApiController]
[Route("api/authors")]
public class AuthorsController: ControllerBase
{
    private readonly IAuthorService _authorService;

    public AuthorsController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAuthorsAsync()
    {
        var authors = await _authorService.GetAllAuthorsAsync();
        return Ok(authors);
    }

    [HttpGet("{id}", Name = "GetAuthor")]
    public async Task<IActionResult> GetAuthorByIdAsync(int id)
    {
        var author = await _authorService.GetAuthorByIdAsync(id);
        if (author == null)
            return NotFound($"Автор с Id {id} не найден.");

        return Ok(author);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAuthorAsync([FromBody] CreateAuthorDto author)
    {
        if (author == null)
            return BadRequest("Данные автора не могут быть пустыми!");
        
        if (string.IsNullOrWhiteSpace(author.Name))
            return BadRequest("Имя автора не может быть пустым!");

        var createdAuthor = await _authorService.AddAuthorAsync(author.ToEntity());
        
        return CreatedAtRoute("GetAuthor", new {id = createdAuthor.Id }, createdAuthor);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAuthorAsync(int id, [FromBody] CreateAuthorDto author)
    {
        if (author == null)
            return BadRequest("Данные автора не могут быть пустыми!");
        
        if (string.IsNullOrWhiteSpace(author.Name))
            return BadRequest("Имя автора не может быть пустым!");

        var updatedAuthor = await _authorService.UpdateAuthorAsync(author.ToEntity(id));
        if (updatedAuthor == null)
            return NotFound($"Автор с Id {id} не найден.");

        return Ok(updatedAuthor);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthorAsync(int id)
    {
        var result = await _authorService.DeleteAuthorAsync(id);
        if (!result)
            return NotFound($"Автор с Id {id} не найден.");

        return NoContent();
    }
}