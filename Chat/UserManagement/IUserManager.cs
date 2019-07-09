using DAL.Models;

namespace Chat.UserManagement
{
    public interface IUserManager
    {
        LoginResult Login(string login, string password);
        User GetUserById(long id);
        SaveUserResult SaveUser(User user);
        void RegisterUser(long userId);
    }
}
