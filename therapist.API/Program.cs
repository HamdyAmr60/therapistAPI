using Microsoft.EntityFrameworkCore;
using therapist.API.projectConfigrations;
using therapist.API.projectConfigrations.Databases;
using Therapist.Reposatories.Data.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.identityConniction();
builder.appDatabase();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.ApplicationService();
var app = builder.Build();
using var Scope = app.Services.CreateScope();
var Services = Scope.ServiceProvider;
await migrationServices.MigrationService(Services,app);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
