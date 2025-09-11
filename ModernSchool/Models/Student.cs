using System.ComponentModel.DataAnnotations;

namespace ModernSchool.Models;

public class Student
{
    [Key]
    public int Id { get; set; }
    [Required, StringLength(50)]
    public string? Nom { get; set; }
    [Required, StringLength(50)]
    public string? Prenom { get; set; }
    [Required, StringLength(1)]
    public string? Genre { get; set; }
    public int ClassId { get; set; }
    public Classe Classe { get; set; }

    public ICollection<Note> Notes {get; set;}
    }