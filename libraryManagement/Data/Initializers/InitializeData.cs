using libraryManagement.Models.Entities;

namespace libraryManagement.Data.Initializers;

public class InitializeData
{
    public static List<Author> InitializeAuthors()
    {
        var authors = new List<Author>
        {
            new Author
            {
                Id = 1,
                Name = "Александр Пушкин",
                DateOfBirth = new DateTime(1799, 6, 6),
                BooksId = new List<int>{3}
            },
            new Author
            {
                Id = 2,
                Name = "Сергей Есенин",
                DateOfBirth = new DateTime(1895, 10, 3),
                BooksId = new List<int>{1, 2}
            }
        };
        return authors;
    }

    public static List<Book> InitializeBooks()
    {
        var books = new List<Book>
        {
            new Book
            {
                Id = 1,
                Title = "Письмо к женщине",
                PublishedYear = 2012,
                AuthorId = 2
            },
            new Book
            {
                Id = 2,
                Title = "Клен ты мой опавший",
                PublishedYear = 2017,
                AuthorId = 2
            },
            new Book
            {
                Id = 3,
                Title = "Евгений Онегин",
                PublishedYear = 2015,
                AuthorId = 1
            },
        };
        return books;
    }
}