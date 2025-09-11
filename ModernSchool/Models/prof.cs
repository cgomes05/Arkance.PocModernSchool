using System.ComponentModel.DataAnnotations;

namespace ModernSchool.Models
{
    public class Professeur
    {
        [Key]
        public int ProfId { get; set; }
        [Required, StringLength(50)]
        public string? Nom { get; set; }
        [Required, StringLength(50)]
        public string? Prenom { get; set; }
        [Required, StringLength(1)]
        public char Genre { get; set; }

        public ICollection<Classe>? ClassesPrincipales { get; set; }
        public ICollection<Enseigne>? Enseignes{ get; set; } 
    }

}