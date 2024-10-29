using System.ComponentModel.DataAnnotations;

namespace AhmedStore.ViewModels
{
    public class LoginVM
    {
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage ="Email is required!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool rememberMe { get; set; }
    }
}
