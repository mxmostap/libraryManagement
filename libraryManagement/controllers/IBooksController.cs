using libraryManagement.models;
using Microsoft.AspNetCore.Mvc;

namespace libraryManagement.controllers;

public interface IBooksController
{
    Task<IActionResult> GetAllBooksAsync();
    Task<IActionResult> GetBookByIdAsync(int id);
    Task<IActionResult> CreateBookAsync([FromBody] Book book);
    Task<IActionResult> UpdateBookAsync(int id, [FromBody] Book book);
    Task<IActionResult> DeleteBookAsync(int id);
}