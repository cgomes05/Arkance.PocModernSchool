using ModernSchool.Models;
using ModernSchool.DataAcces;
using ModernSchool.ViewsModels;
using NSwag;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
namespace ModernSchool;

public static class EntrypointsV1
{
    public static RouteGroupBuilder MapModernSchoolApiV1(this RouteGroupBuilder group)
    {

    //Handler Students
        group.MapGet("/GetAllStudents", GetAllAsync);
        group.MapGet("GetStudentByClasse", GetByClass);
        group.MapPost("AddStudent", RegisterAsync).AddEndpointFilter(async (efiContext, next) =>
        {
            var param = efiContext.GetArgument<StudentItemDTO>(0);
            var validationErrors = Utilities.IsValid(param);
            if (validationErrors.Any())
            {
                return Results.ValidationProblem(validationErrors);
            }
            return await next(efiContext);
        });
        group.MapDelete("/DeleteStudent", DeleteAsync);
        group.MapPut("/UpdateStudent", UpdateAsync);
        group.MapPost("GetStudentNote", GetStudentNoteAsync);

    //Lister les professeur par matière
        group.MapGet("/GetProfByMatiere", GetProfByMatiereAsync);

        //Handler Note
        group.MapPost("/AddNote", AddNoteAsync);
        group.MapPut("/UpdateNote", UpdateNoteAsync);
        group.MapDelete("/DeleteNote", DeleteNoteAsync);


        return group;
    }
    //ALL ENtryPoints Methods
    public static async Task<Ok<Student[]>> GetAllAsync(IStudentService studentService)
    {
        return await studentService.GetAllAsync();
    }

    //Get Student by class
    public static async Task<IResult> GetByClass(int id, AppDbContext Db)
    {
        return await Db.Students.FindAsync(id)
        is Student student ? TypedResults.Ok(new StudentItemDTO(student)) : TypedResults.NotFound();
    }

    static async Task<IResult> RegisterAsync([FromBody] StudentItemDTO model, AppDbContext Db)
    {
        if (model is not null)
        {
            var studentItem = new Student
            {
                Prenom = model.Prenom,
                Nom = model.Nom,
                Genre = model.Genre,
                ClassId =model.ClassId
                
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

    }
    //Lister les professseur par Matiere
    static async Task<Ok<Professeur[]>> GetProfByMatiereAsync()
    {
        throw new NotImplementedException("En cours d'implémentation");
    }

    static async Task<Ok<Note[]>> GetStudentNoteAsync()
    {
        throw new NotImplementedException("En cour d'implementation");
    }
    static async Task<Ok<Note[]>> AddNoteAsync()
    {
        throw new NotImplementedException();
    }

    static async Task UpdateNoteAsync()
    {
        throw new NotImplementedException();
    }
    static async Task DeleteNoteAsync()
    {
        throw new NotImplementedException();
    }
     }