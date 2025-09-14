using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ModernSchool.DataAcces;
using ModernSchool.Models;
using ModernSchool.ViewsModels;

namespace ModernSchool;

public interface IStudentService
{
    Task<IResult> AddStudentAsync(Student model);
    Task<Ok<Student[]>> GetAllAsync();
    Task<IResult> DeleteAsync(int id);
    Task<IResult> UpdateAsync(StudentItemDTO model);
    Task<IResult> GetStudentGradesAsync(int id);
    Task<IResult> GetStudentByClasse();
}

public sealed class StudentService(AppDbContext appDbContext) : IStudentService
{
    private readonly AppDbContext _context = appDbContext;


    public async Task<IResult> AddStudentAsync(Student model)
    {

        try
        {
            _context.Students.Add(model);
            await _context.SaveChangesAsync();
            return TypedResults.Created();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

    }

    public async Task<Ok<Student[]>> GetAllAsync()
    {
        var student = await _context.Students.ToArrayAsync();
        return TypedResults.Ok(student);
    }

    public async Task<IResult> DeleteAsync(int id)
    {
        if (await _context.Students.FindAsync(id) is Student student)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return TypedResults.NoContent();
        }
        return TypedResults.BadRequest();
    }
    public async Task<IResult> UpdateAsync(StudentItemDTO model)
    {
        var ExistingStudent = await _context.Students.FindAsync(model.Id);
        if (ExistingStudent is null) return TypedResults.NotFound();
        ExistingStudent.Nom = model.Nom;
        ExistingStudent.Prenom = model.Prenom;
        await _context.SaveChangesAsync();
        return TypedResults.Created($"/student/v1/{ExistingStudent.Id}", ExistingStudent);
    }

    public async Task<IResult> GetStudentGradesAsync(int id)
    {
     if (id <= 0) 
        return TypedResults.BadRequest("The Id is not valid !");

    // Vérifier si l'étudiant existe (plus efficace que FindAsync + Where)
    var studentExists = await _context.Students.AnyAsync(s => s.Id == id);
    if (!studentExists) 
        return TypedResults.NotFound($"Élève avec l'ID {id} non trouvé.");

        var studentWithHerGrades = _context.Students
                                    .Where(n => n.Id == id)
                                    .Include(n => n.Notes)
                                    .ThenInclude(n => n.Matiere)
                                    .Select(n => new
                                    {
                                        Student = new { n.Id, n.Nom, n.Prenom },
                                        Notes = n.Notes.Select(n => new
                                        {
                                            n.NoteId,
                                            n.Valeur,
                                            Matiere = n.Matiere.Nom
                                        })

                                    })
                                    .FirstOrDefault();
        if (studentWithHerGrades == null)
            return TypedResults.NotFound($"Elève avec {id} non trouvé !");
        return TypedResults.Ok(studentWithHerGrades);
    }

    public async Task<IResult> GetStudentByClasse()
    {
        throw new NotImplementedException();
    }
}