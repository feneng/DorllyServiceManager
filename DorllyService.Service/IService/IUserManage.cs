using DorllyService.Domain;

namespace DorllyService.Service
{
    public interface IUserManage:IRepository<User>
    {
        User Login(string account, string password);
        bool Remove(int userId);
        Account GetAccountByUser(User user);
        Account GetAccountByCookie();
        bool IsAdmin(int userId);
    }
}
