namespace libraryManagement.models;

public class Book
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required int PublishedYear { get; set; }
    public int AuthorId { get; set; }
    //public Author? Author { get; set; }
}