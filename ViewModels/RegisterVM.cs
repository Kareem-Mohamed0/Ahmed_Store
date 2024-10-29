using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AhmedStore.ViewModels
{
    [NotMapped]
    public class RegisterVM
    {
        public string Name { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        public DateOnly BirthDate { get; set; }
        [Required]
        [Key]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Length(3,6,ErrorMessage ="Min length is 3 and Max is 6")]
        public string Pass {  get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Pass")]
        public string ConfirmPass { get; set; }
        [Required]
        public string Phone {  get; set; }
        [Required]
        public string Address { get; set; }
    }
}
