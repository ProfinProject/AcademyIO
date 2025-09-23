using AcademyIO.WebAPI.Core.Configuration;
using AcademyIO.Students.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogger(builder.Configuration);
builder.Services.AddContext(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddSwaggerConfiguration();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwaggerSetup();

app.UseDbMigrationHelper();

app.Run();