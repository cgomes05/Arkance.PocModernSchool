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
    Task<IResult> UpdateAsync(int id, StudentItemDTO model);
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

  public  async Task<Ok<Student[]>> GetAllAsync() {
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
    public async Task<IResult> UpdateAsync(int id, StudentItemDTO model)
    {
        var ExistingStudent = await _context.Students.FindAsync(id);
        if (ExistingStudent is null) return TypedResults.NotFound();
        ExistingStudent.Nom = model.Nom;
        ExistingStudent.Prenom = model.Prenom;
        await _context.SaveChangesAsync();
        return TypedResults.Created($"/student/v1/{ExistingStudent.Id}", ExistingStudent);
    }
}