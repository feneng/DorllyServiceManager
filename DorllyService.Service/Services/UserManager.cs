using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DorllyService.Common;
using DorllyService.Domain;
using Microsoft.Extensions.Logging;

namespace DorllyService.Service
{
    public class UserManager : IUserManager
    {
        private readonly ILogger<UserManager> _logger;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<RolePermission> _rolePermissionReposity;

        public UserManager(IRepository<User> userRepository,
            IRepository<RolePermission> rolePermissionReposity,
            ILogger<UserManager> logger)
        {
            _userRepository = userRepository;
            _rolePermissionReposity = rolePermissionReposity;
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
        public async Task<Account> GetAccountByUser(User user)
        {
            if (user == null) return null;
            try
            {
                var roles = from m in user.UserRoles select m.Role;
                var rolePermissions = await _rolePermissionReposity.LoadEntityListAsNoTrackingAsync(rp => roles.Contains(rp.Role));
                var permissions = rolePermissions.Select(e => e.Permission);
                var modules = permissions.Select(e => e.BelongModule);

                var account = new Account
                {
                    Id = user.Id,
                    Name = user.Name,
                    UserAccount = user.Account,
                    Password = user.Password,
                    AdminIdentity = IsAdmin(user.Id),
                    Avatar = user.Avatar,
                    Garden = user.BelongGarden,
                    Roles = roles,
                    Modules = modules.Distinct(),
                    Permissions = permissions.Distinct(),
                    State = user.State
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
            return _userRepository.LoadEntityEnumerable(e => e.Id == userId).Any(e => e.UserRoles.Any(p => p.RoleId == 1));
        }

        /// <summary>
        /// 登录并返回账户信息
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<Account> Login(string account, string password)
        {
            try
            {
                var salt = GetUserSalt(account);
                if (string.IsNullOrEmpty(salt))
                {
                    return null;
                }
                var newPassword = DesEncrypt.Encrypt(password, salt);
                var user= _userRepository.LoadEntity(m => m.Account == account && m.Password == newPassword);
                return await GetAccountByUser(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Class:UserManager;Method:Login");
                return null;
            }
        }

        public bool Remove(int userId)
        {
            throw new NotImplementedException();
        }

        public string GetUserSalt(string userAccount)
        {
            var user = _userRepository.LoadEntity(m => m.Account == userAccount);
            if (user != null)
            {
                return user.Salt;
            }
            return "";
        }

        public async Task<IEnumerable<Module>> GetModuleByUser(User user)
        {
            var roles = user.UserRoles.Select(e => e.Role);
            var rolePermissions = await _rolePermissionReposity.LoadEntityListAsNoTrackingAsync(rp => roles.Contains(rp.Role));
            var permissions = rolePermissions.Select(e => e.Permission);
            return permissions.Select(e => e.BelongModule).Distinct();
        }
    }
}
