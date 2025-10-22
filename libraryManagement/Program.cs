using System.Text.Json;
using System.Text.Json.Serialization;
using libraryManagement.services;
using libraryManagement.services.implementation;

class Program
{
    //public static string ApplicationName { get; } = "Library management";
    

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        ConfigureServices(builder.Services, builder.Configuration);
        var app = builder.Build();
        ConfigurePipeline(app);
        app.Run();
    }

    static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true;
            });
        services.Configure<RouteOptions>(options =>
        {
            options.LowercaseUrls = true;
        });
        //////////////////////////////////////////////////////////////////////////////////////
        services.AddSingleton<IAuthorService, AuthorService>();
        services.AddSingleton<IBookService, BookService>();
        
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
        app.UseAuthorization();
        app.MapControllers();
    }
}