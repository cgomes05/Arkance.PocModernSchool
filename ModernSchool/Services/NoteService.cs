using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ModernSchool.DataAcces;
using ModernSchool.Models;

namespace ModernSchool;


public interface INoteService
{
    Task AddNoteAsync(int studentId, decimal NoteValeurNote, int matiereId);
    Task<Ok<Note[]>> GetAllNotesAsync();
    Task<IResult> UpdateAsync(int id, Note noteModel);
    Task<IResult> DeleteNoteAsync(int id);
    
}

public sealed class Noteservice(AppDbContext appDbContext) : INoteService
{
    private readonly AppDbContext _context = appDbContext;

    
    /// Méthode pour ajouter une note 
    public async Task AddNoteAsync(int studentId, decimal NoteValeurNote, int matiereId)
    {
        var newNote = new Note
        {
            StudentId = studentId,
            Valeur = NoteValeurNote,
            MatiereId = matiereId,
            DateNote = DateTime.Now
        };
        try
        {
            var result = _context.StudentNote.Add(newNote);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

    }
      
        
    // GetAll Note
    public async Task<Ok<Note[]>> GetAllNotesAsync()
    {
        var newNote = await _context.StudentNote.ToArrayAsync();
        return TypedResults.Ok(newNote);

    }
    //Update Note 
    public async Task<IResult> UpdateAsync(int id, Note noteModel)
    {
        var existingNote = await _context.StudentNote.FindAsync(id);
        if (existingNote is null) return TypedResults.NotFound();
        existingNote.Valeur = noteModel.Valeur;
        existingNote.StudentId = noteModel.StudentId;
        existingNote.MatiereId = noteModel.MatiereId;
        await _context.SaveChangesAsync();
        return TypedResults.Created($"/student/v1/{existingNote.NoteId}", noteModel);
    }

    public async Task<IResult> DeleteNoteAsync(int id)
    {
        await _context.StudentNote.ExecuteDeleteAsync();
        await _context.SaveChangesAsync();
        return TypedResults.Ok();

    }
    

}