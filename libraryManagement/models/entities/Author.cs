namespace libraryManagement.models.entities;

public class Author
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public List<int> BooksId { get; set; }
}