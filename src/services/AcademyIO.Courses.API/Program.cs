using AcademyIO.Core.Enums;
using AcademyIO.Courses.API.Configuration;
using AcademyIO.WebAPI.Core.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddJwtConfiguration(builder.Configuration);

builder.AddContext(EDatabases.SQLite)
    .AddRepositories()
    .AddServices()
    .AddSwaggerConfiguration();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthConfiguration();

app.MapControllers();
app.UseDbMigrationHelper();

app.Run();

public partial class Program { }