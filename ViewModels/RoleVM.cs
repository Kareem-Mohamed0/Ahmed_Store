using System.ComponentModel.DataAnnotations;

namespace AhmedStore.ViewModels
{
    public class RoleVM
    {
        [Key]
        public string RoleName { get; set; }
    }
}
