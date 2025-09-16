using AcademyIO.Auth.API.Configuration;
using AcademyIO.WebAPI.Core.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogger(builder.Configuration);

builder.Services.AddIdentityConfiguration(builder.Configuration);

builder.Services.AddApiConfiguration(builder.Configuration);

builder.Services.AddSwaggerConfiguration();

builder.Services.AddMessageBusConfiguration(builder.Configuration);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//DbMigrationHelpers.EnsureSeedData(app).Wait();


app.UseSwaggerSetup();

app.UseApiConfiguration(app.Environment);

app.Run();
