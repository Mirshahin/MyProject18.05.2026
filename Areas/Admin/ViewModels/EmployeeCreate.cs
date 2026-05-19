using System.ComponentModel.DataAnnotations;

namespace MyProject18._05._2026.Areas.Admin.ViewModels
{
    public class EmployeeCreate
    {
        [Required(ErrorMessage = "Name is required.")]
        [
            StringLength(15, ErrorMessage = "Name cannot be longer than 15 characters."),
            MinLength(3, ErrorMessage = "Name must be at least 3 characters long.")
        ]
        public string Name { get; set; }
       
        [Required(ErrorMessage = "Surname is required.")]
        [
         StringLength(15, ErrorMessage = "Surname cannot be longer than 15 characters."),
         MinLength(3, ErrorMessage = "Surname must be at least 3 characters long.")
     ]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Age is required.")]
        [
         Range(18, 65, ErrorMessage = "Age must be between 18 and 65.")
     ]
        public int Age { get; set; }
        [Required(ErrorMessage = "Position is required.")]
        [
         StringLength(15, ErrorMessage = "Position cannot be longer than 15 characters."),
         MinLength(3, ErrorMessage = "Position must be at least 3 characters long.")
     ]
        public string Position { get; set; }
        [Required(ErrorMessage = "RangeId is required.")]
        [
         Range(1, int.MaxValue, ErrorMessage = "RangeId must be a positive number.")
     ]
        public int RangeId { get; set; }
    }
}
