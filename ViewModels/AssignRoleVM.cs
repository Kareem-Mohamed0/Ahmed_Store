using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AhmedStore.ViewModels
{
    public class AssignRoleVM
    {
        public string UserId { get; set; }
        [Key]
        public string RoleId { get; set; }
        [NotMapped]
        public List<SelectListItem>? RoleList { get; set; }
    }
}
