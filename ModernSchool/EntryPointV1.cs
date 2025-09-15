using ModernSchool.Models;
using ModernSchool.DataAcces;
using ModernSchool.ViewsModels;
using NSwag;
using Microsoft.AspNetCore.Http.HttpResults;
namespace ModernSchool;

public static class EntrypointsV1
{
    public static RouteGroupBuilder MapModernSchoolApiV1(this RouteGroupBuilder group)
    {

    //Handler Students
        group.MapGet("/GetAllStudents", GetAllAsync);
        group.MapGet("GetStudentByClasse", GetStudentByClasse);
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

    //Lister les eleves par classe
    public static async Task<IResult> GetStudentByClasse(IStudentService studentService)
    {

        return await studentService.GetStudentByClasseAsync();
    }

    static async Task<IResult> RegisterAsync(StudentItemDTO model, IStudentService Db)
    {
        if (model is not null)
        {
            var studentItem = new Student
            {
                Prenom = model.Prenom,
                Nom = model.Nom,
                Genre = model.Genre,
            };
           await Db.AddStudentAsync(studentItem);
            return TypedResults.Created();
        }
        ;
        return TypedResults.BadRequest();
    }

    static async Task<IResult> DeleteAsync(int id, IStudentService studentService)
    {
        await studentService.DeleteAsync(id);
        return TypedResults.Ok($"Utilisateur avec Id {id} a bien été supprimé !");

    }

    static async Task<IResult> UpdateAsync(StudentItemDTO model, IStudentService studentService)
    {
         await studentService.UpdateAsync(model);
        return TypedResults.Created();

    }
    //Lister les professseur par Matiere
    static async Task<IResult> GetProfByMatiereAsync(IProfService profService)
    {
        return await profService.GetProfByMatiereAsync();

    }

    static async Task<IResult> GetStudentNoteAsync(int id, IStudentService studentService)
    {
        var getResult = await studentService.GetStudentGradesAsync(id);
        return TypedResults.Ok(getResult);
        
    }
    static async Task<IResult> AddNoteAsync(int studentId, decimal NoteValeur, int matiereId, INoteService noteService)
    {
        // Validation rapide
    if (NoteValeur < 0 || NoteValeur > 20)
        return Results.ValidationProblem(
            new Dictionary<string, string[]>
            {
                { "Valeur", new[] { "La note doit être entre 0 et 20" } }
            });
            
        await noteService.AddNoteAsync(studentId, NoteValeur, matiereId);
        var noteModel = new Note
        {
            StudentId = studentId,
            Valeur = NoteValeur,
            MatiereId = matiereId

        };
        return TypedResults.Ok(noteModel);
    }

    static async Task<IResult> UpdateNoteAsync(int idNote, decimal valeurNote, INoteService noteService)
    {
        await noteService.UpdateAsync(idNote, valeurNote);
        return TypedResults.Ok();
    }
    static async Task<IResult> DeleteNoteAsync(int idNote, INoteService noteService)
    {
        var result = await noteService.DeleteNoteAsync(idNote);
        return TypedResults.Ok(result);
    }
     }