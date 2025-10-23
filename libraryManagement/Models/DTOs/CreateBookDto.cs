namespace libraryManagement.Models.DTOs;

public class CreateBookDto
{
    public required string Title { get; set; }
    public required int PublishedYear { get; set; }
    public int AuthorId { get; set; }
}