using System.ComponentModel.DataAnnotations;

namespace AhmedStore.ViewModels
{
    public class EditUserVM
    {
        [Key]
        [Required(ErrorMessage="The Name is required")]
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        [Required(ErrorMessage = "The BirthDate is required")]
        public DateOnly BirthDate { get; set; }
        [Required(ErrorMessage = "The Email is required")]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
