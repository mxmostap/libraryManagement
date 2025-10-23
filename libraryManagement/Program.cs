using libraryManagement.extensions.dependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureServices();

var app = builder.Build();
app.ConfigurePipeline();

app.Run();
