using System.Text.Json.Serialization;
using libraryManagement.models.entities;
using libraryManagement.repositories;
using libraryManagement.repositories.implementation;
using libraryManagement.services;
using libraryManagement.services.implementation;
using libraryManagement.storage;
using libraryManagement.storage.implementation;

var builder = WebApplication.CreateBuilder(args);
ConfigureServices(builder.Services, builder.Configuration);
var app = builder.Build();
ConfigurePipeline(app);
app.Run();


static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.WriteIndented = true;
        });
    services.Configure<RouteOptions>(options => { options.LowercaseUrls = true; });
    
    var authorStorage = new TemporaryStorage<Author>(InitializeAuthors());
    var bookStorage = new TemporaryStorage<Book>(InitializeBooks());

    services.AddSingleton<ITemporaryStorage<Author>>(authorStorage);
    services.AddSingleton<ITemporaryStorage<Book>>(bookStorage);
    
    
    services.AddScoped<IAuthorRepository, AuthorRepository>();
    services.AddScoped<IBookRepository, BookRepository>();

    services.AddScoped<IAuthorService, AuthorService>();
    services.AddScoped<IBookService, BookService>();

    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "Library API",
            Version = "v1",
            Description = "Web API для управления библиотекой"
        });
    });
}

static void ConfigurePipeline(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    //app.UseAuthorization();
    app.MapControllers();
}

static List<Author> InitializeAuthors()
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

static List<Book> InitializeBooks()
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