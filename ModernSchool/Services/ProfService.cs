using Microsoft.EntityFrameworkCore;
using ModernSchool.DataAcces;

namespace ModernSchool;

public interface IProfService
{
    Task<IResult> GetProfByMatiereAsync();
}

public sealed class ProfService(AppDbContext appDbContext) : IProfService
{
    private readonly AppDbContext _context = appDbContext;
    public async Task<IResult> GetProfByMatiereAsync()
    {
        var result = await _context.Matieres
        .Select(m => new
        {
            Matiere = m.Nom,
            Professeurs = m.Enseignes
                .Select(e => e.Professeur)
                .OrderBy(p => p.Nom)
                .Select(p => new 
                { 
                    p.ProfId, 
                    p.Nom, 
                    p.Prenom 
                })
                .ToList()
        })
        .OrderBy(x => x.Matiere)
        .ToListAsync();

    return TypedResults.Ok(result);

    }
}