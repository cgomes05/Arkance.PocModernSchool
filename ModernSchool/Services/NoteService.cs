using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ModernSchool.DataAcces;
using ModernSchool.Models;

namespace ModernSchool;


public interface INoteService
{
    Task AddNoteAsync(int studentId, decimal NoteValeurNote, int matiereId);
    Task<Ok<Note[]>> GetAllNotesAsync();
    Task<IResult> UpdateAsync(int id, decimal valeurNote);
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
            //DateNote = DateTime.Now
        };
        try
        {
            var result = _context.Notes.Add(newNote);
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
        var newNote = await _context.Notes.ToArrayAsync();
        return TypedResults.Ok(newNote);

    }
    //Update Note 
    public async Task<IResult> UpdateAsync(int id, decimal valeurNote)
    {
        var existingNote = await _context.Notes.FindAsync(id);
        if (existingNote is null) return TypedResults.NotFound();
        existingNote.Valeur = valeurNote;
        //existingNote.StudentId = noteModel.StudentId;
        //existingNote.MatiereId = noteModel.MatiereId;
        await _context.SaveChangesAsync();
        return TypedResults.Created($"/student/v1/{existingNote.NoteId}", existingNote);
    }

    public async Task<IResult> DeleteNoteAsync(int id)
    {

        var existingNote = await _context.Notes.FindAsync(id);
        if (existingNote != null)
        {
            await _context.Notes.ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
            return TypedResults.Ok();
        }
        return TypedResults.NotFound("This Id note is not exist !");
        

    }
    

}