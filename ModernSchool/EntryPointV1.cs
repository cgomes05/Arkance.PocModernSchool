using ModernSchool.Models;
using ModernSchool.DataAcces;
using ModernSchool.ViewsModels;
using NSwag;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
namespace ModernSchool;
public static class EntrypointsV1
{
    public static RouteGroupBuilder MapModernSchoolApiV1(this RouteGroupBuilder group) {


        group.MapGet("/getAll", GetAllAsync);
        group.MapGet("GetById", GetByIdAsync);
        group.MapPost("register", RegisterAsync).AddEndpointFilter(async (efiContext, next)=>
        {
            var param = efiContext.GetArgument<StudentItemDTO>(0);
            var validationErrors = Utilities.IsValid(param);
            if (validationErrors.Any())
            {
                return Results.ValidationProblem(validationErrors);
            }
            return await next(efiContext);
        });
        group.MapDelete("/delete", DeleteAsync);
        group.MapPut("/Update", UpdateAsync);
        return group;
    }
    //ALL ENtryPoints Methods
    public static async Task<Ok<Student[]>> GetAllAsync(AppDbContext Db) {
        var student = await Db.Students.ToArrayAsync();
        return TypedResults.Ok(student);
    }

    public static async Task<IResult> GetByIdAsync(int id, AppDbContext Db)
    {
        return await Db.Students.FindAsync(id)
        is Student student ? TypedResults.Ok(new StudentItemDTO(student)) : TypedResults.NotFound();
    }

    static async Task<IResult> RegisterAsync(StudentItemDTO model, AppDbContext Db)
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

    static async Task<IResult> DeleteAsync(int id, AppDbContext db)
    {
        if (await db.Students.FindAsync(id) is Student student)
        {
            db.Students.Remove(student);
            await db.SaveChangesAsync();
            return TypedResults.NoContent();
        }
        return TypedResults.BadRequest();

    }

    static async Task<IResult> UpdateAsync(int id, StudentItemDTO model, AppDbContext db)
    {
        var ExistingStudent = await db.Students.FindAsync(id);
        if (ExistingStudent is null) return TypedResults.NotFound();
        ExistingStudent.Nom = model.Nom;
        ExistingStudent.Prenom = model.Prenom;
        await db.SaveChangesAsync();
        return TypedResults.Created($"/student/v1/{ExistingStudent.Id}", ExistingStudent);

    } }