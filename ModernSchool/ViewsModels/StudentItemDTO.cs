using System.ComponentModel.DataAnnotations;
using ModernSchool.Models;

namespace ModernSchool.ViewsModels;
    public class StudentItemDTO
    {
    [Key]
    public int Id { get; set; }
    [Required, StringLength(50)]
    public string? Nom { get; set; }
    [Required, StringLength(50)]
    public string? Prenom { get; set; }
    [Required, StringLength(1)]
    public string? Genre { get; set; }
    


    //public ICollection<Note> Notes {get; set;}
        public StudentItemDTO() { }

        public StudentItemDTO(Student studentDTO) =>
        (Id, Nom, Prenom,Genre) = (studentDTO.Id, studentDTO.Nom, studentDTO.Prenom, studentDTO.Genre);

    }