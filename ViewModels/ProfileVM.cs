using AhmedStore.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AhmedStore.ViewModels
{
    [NotMapped]
    public class ProfileVM
    {
        [Key]
        public string Name { get; set; }
        public string Image { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public static implicit operator ProfileVM(RegisterVM v)
        {
            throw new NotImplementedException();
        }
    }
}
