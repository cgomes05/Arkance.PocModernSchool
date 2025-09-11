using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ModernSchool.Models;

public class Enseigne
{
    //Clé composite : professeur + MatiereId
    public int ProfId { get; set; }
    public Professeur? Professeur { get; set; } 
    public int MatiereId { get; set; }
    public Matiere? Matiere{ get; set; }
}