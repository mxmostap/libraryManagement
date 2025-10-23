using System.Text.Json.Serialization;
using libraryManagement.Data.Initializers;
using libraryManagement.Data.TemporaryStorage;
using libraryManagement.Data.TemporaryStorage.Implementation;
using libraryManagement.Models.Entities;
using libraryManagement.Repositories;
using libraryManagement.Repositories.Implementation;
using libraryManagement.Services;
using libraryManagement.Services.Implementation;

namespace libraryManagement.Extensions;

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
        app.UseGlobalErrorHandling();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.MapControllers();
    }
}