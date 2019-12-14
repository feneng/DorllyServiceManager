using System;
using System.Linq;
using DorllyService.Domain;
using Microsoft.Extensions.Logging;

namespace DorllyService.Service
{
    public class UserManager:Repository<User>,IUserManage
    {
        private readonly DorllyServiceManagerContext _context;
        private readonly ILogger<UserManager> _logger;

        public UserManager(DorllyServiceManagerContext context, ILogger<UserManager> logger) :base(context)
        {
            _context = context;
            _logger = logger;
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
            if (user == null) return null;
            try
            {
                var roles = from m in user.UserRoles select m.Role;
                var rolePermissions= _context.RolePermission.Where(rp => roles.Contains(rp.Role));
                var permissions = rolePermissions.Select(e => e.Permission);
                var modules = permissions.Select(e => e.BelongModule);

                var account = new Account
                {
                    Id = user.Id,
                    Name = user.Name,
                    LogName = user.Account,
                    Password = user.Password,
                    AdminIdentity = IsAdmin(user.Id),
                    Avatar = user.Avatar,
                    Garden = user.BelongGarden,
                    Roles=roles,
                    Modules=modules.Distinct(),
                    Permissions=permissions.Distinct()
                };
                return account;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Class:UserManager;Method:GetAccountByUser");
                return null;
            }
        }

        public bool IsAdmin(int userId)
        {
            return base.LoadEntityEnumerable(e => e.Id == userId).Any(e => e.UserRoles.Any(p => p.RoleId == 1));
        }

        public User Login(string account, string password)
        {
            return base.LoadEntity(m => m.Account == account && m.Password == password);
        }

        public bool Remove(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
