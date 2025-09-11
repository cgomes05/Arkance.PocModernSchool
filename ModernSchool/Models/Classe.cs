
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using ModernSchool.DataAcces;
namespace ModernSchool.Models;

public class Classe
{
    [Key]
    public int ClassId { get; set; }
    [Required]
    [StringLength(25)]
    public string? NomNiveau { get; set; }
    public int ProfPrincipalId { get; set; }
    public Professeur? ProfPrincipal { get; set; }
    public DateTime DatCreation { get; set; } = DateTime.Now;
    public DateTime DteUpdate { get; set; } = DateTime.Now;
    public ICollection<Student>? Students { get; set; }

}