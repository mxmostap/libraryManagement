namespace libraryManagement.Models.DTOs;

public class CreateAuthorDto
{
    public required string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public List<int> BooksId { get; set; }
}