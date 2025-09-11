using System.ComponentModel.DataAnnotations;

namespace ModernSchool.Models;

public class Note
{
    [Key]
    public int NoteId { get; set; }

    [Required, Range(0, 20)]
    public decimal Valeur { get; set; }

    public int StudentId { get; set; }
    public Student? Student { get; set; }

    public int MatiereId { get; set; }
    public Matiere? Matiere { get; set; }
    public DateTime DateNote { get; set; } = DateTime.Now;

}