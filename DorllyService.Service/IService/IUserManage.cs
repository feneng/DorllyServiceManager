using System.Collections.Generic;
using System.Threading.Tasks;
using DorllyService.Domain;

namespace DorllyService.Service
{
    public interface IUserManager
    {
        Task<Account> Login(string account, string password);
        bool Remove(int userId);
        Task<Account> GetAccountByUser(User user);
        Account GetAccountByCookie();
        bool IsAdmin(int userId);
        string GetUserSalt(string account);
        /// <summary>
        /// 获取用户的模块列表
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<IEnumerable<Module>> GetModuleByUser(User user);
    }
}
