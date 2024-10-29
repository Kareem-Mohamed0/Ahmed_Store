using AhmedStore.ViewModels;

namespace AhmedStore.Repository
{
    public interface IUserRepository
    {
        public List<DisplayUsersVM> DisplayUsers();
    }
}
