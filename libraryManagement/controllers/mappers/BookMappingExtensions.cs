using libraryManagement.models;
using libraryManagement.models.DTOs;
using libraryManagement.models.entities;

namespace libraryManagement.controllers.mappers;

public static class BookMappingExtensions
{
    public static Book ToBook(this CreateBookDto dto)
    {
        return new Book
        {
            Title = dto.Title.Trim(),
            PublishedYear = dto.PublishedYear,
            AuthorId = dto.AuthorId
        };
    }
    
    public static Book ToUpdateBook(this CreateBookDto dto, int id)
    {
        return new Book
        {
            Id = id,
            Title = dto.Title.Trim(),
            PublishedYear = dto.PublishedYear,
            AuthorId = dto.AuthorId
        };
    }
}