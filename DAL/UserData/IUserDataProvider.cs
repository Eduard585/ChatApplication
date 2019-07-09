namespace DAL.UserData
{
    public interface IUserDataProvider
    {
        Models.User GetUserById(long userId);
        long SaveUser(Models.User user);
    }
}
