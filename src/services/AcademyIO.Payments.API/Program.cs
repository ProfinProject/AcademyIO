using AcademyIO.Payments.API.Configuration;
using AcademyIO.WebAPI.Core.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogger(builder.Configuration);
builder.Services.AddApiCoreConfiguration(builder.Configuration);
builder.Services.AddContext(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddSwaggerConfiguration();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwaggerSetup();
app.UseApiCoreConfiguration(app.Environment);
app.UseDbMigrationHelper();

app.Run();
