using libraryManagement.models.DTOs;
using libraryManagement.models.entities;

namespace libraryManagement.extensions.mapping;

public static class AuthorMappingExtensions
{
    public static Author ToEntity(this CreateAuthorDto dto)
    {
        return new Author
        {
            Name = dto.Name.Trim(),
            DateOfBirth = dto.DateOfBirth,
            BooksId = dto.BooksId ?? new List<int>()
        };
    }
    
    public static Author ToEntity(this CreateAuthorDto dto, int id)
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