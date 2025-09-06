using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using ModernSchool.DataAcces;
using ModernSchool.Models;
using ModernSchool.ViewsModels;


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
var StudentItems = app.MapGroup("/students");
    StudentItems.MapGet("/getAll", GetAllAsync);
    StudentItems.MapGet("GetById", GetByIdAsync);
    StudentItems.MapPost("register", RegisterAsync);
    StudentItems.MapDelete("/delete", DeleteAsync);
    StudentItems.MapPut("/Update", UpdateAsync);

app.Run();

 static async Task<IResult> GetAllAsync(CustomDbContext Db) {
    return TypedResults.Ok(await Db.Students.Select(x => new StudentItemDTO(x)).ToArrayAsync());
};

static async Task<IResult> GetByIdAsync(int id,CustomDbContext Db)
{
    return await Db.Students.FindAsync(id)
    is Student student ? TypedResults.Ok(new StudentItemDTO(student)) : TypedResults.NotFound();
}

static async Task<IResult> RegisterAsync(StudentItemDTO model, CustomDbContext Db)
{
    if (model is not null)
    {
        var studentItem = new Student
        {
            Prenom = model.Prenom,
            Nom = model.Nom
        };
        Db.Students.Add(studentItem);
        await Db.SaveChangesAsync();
        return TypedResults.Created();
    }
    ;
    return TypedResults.BadRequest();
}

static async Task<IResult> DeleteAsync(int id, CustomDbContext db)
{
    if (await db.Students.FindAsync(id) is Student student)
    {
        db.Students.Remove(student);
        await db.SaveChangesAsync();
        return TypedResults.NoContent();
    }
    return TypedResults.BadRequest();

}

static async Task<IResult> UpdateAsync(int id, StudentItemDTO model, CustomDbContext db)
{
    var student = await db.Students.FindAsync(id);
    if (student is null) return TypedResults.NotFound();
    student.Nom = model.Nom;
    student.Prenom = model.Prenom;
    await db.SaveChangesAsync();
    return TypedResults.NoContent();

}
{
    
}
{
    
}