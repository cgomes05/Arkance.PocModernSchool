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
        var result = await _context.Enseignes
        .Include(e => e.Professeur)
        .Include(e => e.Matiere)
        .Select(e => new
        {
            Profid = e.Professeur.ProfId,
            Nom = e.Professeur.Nom,
            Prenom = e.Professeur.Prenom,
            NomDeLaMatiere = e.Matiere.Nom
        })
        .OrderBy(x => x.NomDeLaMatiere)
        .ThenBy(x => x.Nom)
        .ToListAsync();
        return TypedResults.Ok(result);

    }
}