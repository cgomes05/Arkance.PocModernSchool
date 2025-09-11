using Microsoft.EntityFrameworkCore;
using ModernSchool.DataAcces;
using ModernSchool.Models;
namespace ModernSchool;

public interface IClasseService
{
    Task<List<Student>> GetStudentWithTheirNoteAsync(string classLevel);
}
public sealed class ClasseService(AppDbContext appDbContext) : IClasseService
{
    private readonly AppDbContext _context = appDbContext;

    //Methode pour récuper les eleves d'une classe avec leur notes

    public async Task<List<Student>> GetStudentWithTheirNoteAsync(string classLevel)
    {
        try
        {
            return await _context.Students
               .Include(e => e.Classe)
               .Include(e => e.Notes)
               .ThenInclude(n => n.Matiere)
               .Where(e => e.Classe.NomNiveau == classLevel)
               .ToListAsync();
        }
        catch (Exception e)
        {
            throw new NotImplementedException($"The errorMessage :{e.Message}");
        }


    }




}