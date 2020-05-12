using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DorllyService.Domain;

namespace DorllyService.Service
{
    public interface IUserManager:IRepository<User>
    {
        IQueryable<User> GetIndexQuery();
        Account Login(string account, string password);
        Task<bool> Remove(int userId);
        Account GetAccountByUser(User user);
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
