using System.Text.Json.Serialization;
using libraryManagement.data.initializers;
using libraryManagement.data.temporaryStorage;
using libraryManagement.data.temporaryStorage.implementation;
using libraryManagement.models.entities;
using libraryManagement.repositories;
using libraryManagement.repositories.implementation;
using libraryManagement.services;
using libraryManagement.services.implementation;

namespace libraryManagement.extensions.dependencyInjection;

public static class DependencyInjectionExtensions
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true;
            });
        services.Configure<RouteOptions>(options => { options.LowercaseUrls = true; });
    
        var authorStorage = new TemporaryStorage<Author>(InitializeData.InitializeAuthors());
        var bookStorage = new TemporaryStorage<Book>(InitializeData.InitializeBooks());

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

    public static void ConfigurePipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.MapControllers();
    }
}