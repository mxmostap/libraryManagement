using libraryManagement.models;
using libraryManagement.models.DTOs;
using libraryManagement.models.entities;

namespace libraryManagement.controllers.mappers;

public static class AuthorMappingExtensions
{
    public static Author ToAuthor(this CreateAuthorDto dto)
    {
        return new Author
        {
            Name = dto.Name.Trim(),
            DateOfBirth = dto.DateOfBirth,
            BooksId = dto.BooksId ?? new List<int>()
        };
    }
    
    public static Author ToUpdateAuthor(this CreateAuthorDto dto, int id)
    {
        return new Author
        {
            Id = id,
            Name = dto.Name.Trim(),
            DateOfBirth = dto.DateOfBirth,
            BooksId = dto.BooksId ?? new List<int>()
        };
    }
}