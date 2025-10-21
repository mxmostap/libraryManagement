using libraryManagement.models;
using Microsoft.AspNetCore.Mvc;

namespace libraryManagement.controllers;

public interface IAuthorsController
{
    Task<IActionResult> GetAllAuthorsAsync();
    Task<IActionResult> GetAuthorByIdAsync(int id);
    Task<IActionResult> CreateAuthorAsync([FromBody] Author author);
    Task<IActionResult> UpdateAuthorAsync(int id, [FromBody] Author author);
    Task<IActionResult> DeleteAuthorAsync(int id);
}