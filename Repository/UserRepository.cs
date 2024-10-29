using AhmedStore.Data;
using AhmedStore.Models;
using AhmedStore.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AhmedStore.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> userManager;
        private readonly AppDbContext context;

        public UserRepository(UserManager<User> userManager,AppDbContext context)
        {
            this.userManager = userManager;
            this.context = context;
        }
        public List<DisplayUsersVM> DisplayUsers()
        {
            var Users = context.Users
                .Include(U => U.Shops)
                .Select(S => new DisplayUsersVM
                {
                    Id = S.Id,
                    BirthDate = S.BirthDate,
                    Address = S.Address,
                    Email = S.Email,
                    Name = S.UserName,
                    Phone = S.PhoneNumber,
                    ShopName = S.Shops.FirstOrDefault() !=null ? S.Shops.FirstOrDefault().Name : " "
                }).ToList();
            return Users;
        }
    }
}
