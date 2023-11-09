using Reservations.Api;
using Reservations.Api.Helpers;
using Shared.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.DocumentFilter<RemoveCAPRoutes>();
});

builder.Services.AddApi(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.UseCustomExceptionHandler();

app.UseAuthorization();

app.MapControllers();

app.AddDBMigrations();

app.Run();

public partial class Program { }
