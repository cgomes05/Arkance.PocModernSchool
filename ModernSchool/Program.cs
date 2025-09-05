using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using ModernSchool.DataAcces;
using ModernSchool.Models;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CustomDbContext>(opt => opt.UseInMemoryDatabase("StudentsList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "ModernSchoolAPI";
    config.Title = "ModernSchoolAPI V1";
    config.Version = "V1";
});
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "ModernSchoolAPI";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}
app.MapGet("/getAll", async (CustomDbContext Db) =>
await Db.Students.ToListAsync()
);

app.MapPost("/register", async (Student model, CustomDbContext Db) =>
{
    Db.Students.Add(model);
await Db.SaveChangesAsync();
return Results.Created($"/student/{model.Id}", model);
});


app.MapGet("/getbyId/{id}",async(int id, CustomDbContext Db) =>
    await Db.Students.FindAsync(id)
    is Student model ? Results.Ok(model) : Results.NotFound());

app.MapPut("/put/{id}", async (int id,Student model, CustomDbContext Db) =>
{
    var student = await Db.Students.FindAsync(id);
    if (student is null) return Results.NotFound();
    model.Nom = student.Nom;
    model.Prenom = student.Prenom;
    await Db.SaveChangesAsync();
    return Results.NoContent();
});


app.MapDelete("/delete/{id}", async (int id, CustomDbContext Db) =>
{
    if (await Db.Students.FindAsync(id) is Student model)
    {
        Db.Students.Remove(model);
        await Db.SaveChangesAsync();
        return Results.NoContent();
    }
    return Results.NotFound();

});
app.Run();
