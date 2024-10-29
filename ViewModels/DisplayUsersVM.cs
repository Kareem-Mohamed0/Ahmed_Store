using System.ComponentModel.DataAnnotations;

namespace AhmedStore.ViewModels
{
    public class DisplayUsersVM
    {
        public string Id { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        [Key]
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Age
        {
            get
            {
                var today = DateOnly.FromDateTime(DateTime.Now);
                int age = today.Year - BirthDate.Year;
                return age;
            }
            
        }
        public string? ShopName { get; set; }

    }
}
