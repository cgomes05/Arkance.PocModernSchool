using System.ComponentModel.DataAnnotations;

namespace ModernSchool.Models;

public class Matiere
{
    [Key]
    public int MatiereId { get; set; }
    [Required, StringLength(50)]
    public string? Nom { get; set; }

    public ICollection<Note>? Notes { get; set; }
    public ICollection<Enseigne>? Enseignes{ get; set; }
}