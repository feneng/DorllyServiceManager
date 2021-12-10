using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DorllyService.Common;
using DorllyService.Common.Extensions;
using DorllyService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using Z.EntityFramework.Plus;
using DorllyService.IService;

namespace DorllyService.Service
{
    public class UserManager :Repository<User>, IUserManager
    {
        private readonly DorllyServiceManagerContext _context;
        private readonly ILogger<UserManager> _logger;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<UserRole> _userRoleRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<RolePermission> _rolePermissionReposity;
        private readonly IRepository<Permission> _permissionRepository;
        private readonly IRepository<Module> _moduleRepository;


        public UserManager(IRepository<User> userRepository,
            IRepository<UserRole> userRoleRepository,
            IRepository<Role> roleRepository,
            IRepository<RolePermission> rolePermissionReposity,
            IRepository<Permission> permissionRepository,
            IRepository<Module> moduleRepository,
            DorllyServiceManagerContext context,
            ILogger<UserManager> logger) : base(context)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
            _rolePermissionReposity = rolePermissionReposity;
            _permissionRepository = permissionRepository;
            _moduleRepository = moduleRepository;
            _context = context;
            _logger = logger;
        }

        public Account GetAccountByCookie()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get account info by user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Account GetAccountByUser(User user)
        {
            if (user == null) return null;
            try
            {
                var queryUser = _context.User
                    .Where(u => u.Id == user.Id)
                    .Include(u => u.UserRoles)
                        .ThenInclude(ur=>ur.Role)
                            .ThenInclude(r=>r.RolePermissions)
                                .ThenInclude(rp=>rp.Permission)
                                    .ThenInclude(p=>p.BelongModule)
                    .AsNoTracking()
                    .Single();

                var roles = queryUser.UserRoles.Select(ur => ur.Role);
                var permissions=roles.SelectMany(r => r.RolePermissions)
                    .Select(e => e.Permission).Distinct(new ModelDistinct<Permission>("Id"));

                var modules = permissions.Select(p => p.BelongModule).Distinct(new ModelDistinct<Module>("Id"));
                var parentModules = GetParentModules(modules);
                 while (parentModules.Any())
                {
                    modules = modules.Concat(parentModules);
                    parentModules = GetParentModules(parentModules);
                }

                var account = new Account
                {
                    Id = user.Id,
                    Name = user.Name,
                    UserAccount = user.Account,
                    Password = user.Password,
                    AdminIdentity = IsAdmin(user.Id),
                    Avatar = user.Avatar,
                    Garden = user.BelongGarden,
                    Email=user.Email,
                    Roles = roles,
                    Modules = modules.ToTreeStruct(null).Distinct(new ModelDistinct<Module>("Id")),
                    Permissions = permissions,
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

        private IEnumerable<Module> GetParentModules(IEnumerable<Module> moduleList)
        {
            return _context.Module.Include(m => m.Parent)
                .Where(m => moduleList.Contains(m)&&m.Parent!=null)
               .AsNoTracking()
               .Select(m => m.Parent);
        }

        /// <summary>
        /// 是否管理员
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool IsAdmin(int userId)
        {
            return _userRepository.LoadEntityEnumerable(e => e.UserType == 1).Any(e=>e.Id==userId);
        }

        /// <summary>
        /// 登录并返回账户信息
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Account Login(string account, string password)
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
                return GetAccountByUser(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Class:UserManager;Method:Login");
                return null;
            }
        }

        public async Task<bool> Remove(int userId)
        {
            try
            {
                var user = await _context.User.Include(i => i.UserRoles).SingleAsync(u => u.Id == userId);
                if (user.UserRoles.Count > 0)
                {
                    await _userRoleRepository.DelEntityAsync(ur => user.UserRoles.Contains(ur));
                }
                await _userRepository.DelEntityAsync(u => u.Id == userId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Class:UserManager;Method:Remove");
                return false;
            }
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

        public IQueryable<User> GetIndexQuery()
        {
            return _context.User.Include(r => r.BelongGarden).Include(r => r.Supplier).AsNoTracking();
        }

        public override async Task<User> LoadEntityAsNoTrackingAsync(Expression<Func<User, bool>> predicate)
        {
            return await _context.User
                .Include(u => u.BelongGarden)
                .Include(r => r.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .Where(predicate)
                .AsNoTracking()
                .SingleOrDefaultAsync();
        }

        IService.Account IUserManager.Login(string account, string password)
        {
            throw new NotImplementedException();
        }

        IService.Account IUserManager.GetAccountByUser(User user)
        {
            throw new NotImplementedException();
        }

        IService.Account IUserManager.GetAccountByCookie()
        {
            throw new NotImplementedException();
        }
    }
}
