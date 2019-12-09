using System;
using System.Linq;
using DorllyService.Domain;

namespace DorllyService.Service
{
    public class UserManager:Repository<User>,IUserManage
    {
        private readonly DorllyServiceManagerContext _context;

        public UserManager(DorllyServiceManagerContext context):base(context)
        {
            _context = context;
        }

        public Account GetAccountByCookie()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取用户账号信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Account GetAccountByUser(User user)
        {
            if (user is null)
            {
                return null;
            }
            try
            {
                var account = new Account
                {
                    Id = user.Id,
                    Name = user.Name,
                    LogName = user.Account,
                    Password = user.Password,
                    AdminIdentity = IsAdmin(user.Id),
                    Avatar = user.Avatar,
                    Garden = user.BelongGarden,
                    //Roles=user.UserRoles.ToList(),
                };
                return account;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool IsAdmin(int userId)
        {
            return base.LoadEntityEnumerable(e => e.Id == userId).Any(e => e.UserRoles.Any(p => p.RoleId == 1));
        }

        public User Login(string account, string password)
        {
            throw new NotImplementedException();
        }

        public bool Remove(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
