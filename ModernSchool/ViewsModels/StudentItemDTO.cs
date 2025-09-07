using ModernSchool.Models;

namespace ModernSchool.ViewsModels;
    public class StudentItemDTO
    {
        public  int Id { get; set; }
        public  string? Nom { get; set; }
        public  string? Prenom { get; set; }
        public StudentItemDTO() { }

        public StudentItemDTO(Student studentDTO) =>
        (Id, Nom, Prenom) = (studentDTO.Id, studentDTO.Nom, studentDTO.Prenom);

    }