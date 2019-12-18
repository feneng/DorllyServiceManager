using DorllyService.Domain;
using System.Collections.Generic;
using System.Linq;

namespace DorllyService.Service
{
    /// <summary>
    /// 通用用户登录类，简单信息
    /// </summary>
    public class Account
    {
        #region Attribute
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 登录的用户名
        /// </summary>
        public string UserAccount { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 管理员标识
        /// </summary>
        public bool AdminIdentity { get; set; }
        /// <summary>
        /// 用户头像
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// 用户所属系统
        /// </summary>
        public Garden Garden { get; set; }
        /// <summary>
        /// 用户状态
        /// </summary>
        public byte State { get; set; }
        /// <summary>
        /// 权限集合
        /// </summary>
        public IEnumerable<Permission> Permissions { get; set; }
        /// <summary>
        /// 角色的集合
        /// </summary>
        public IEnumerable<Role> Roles { get; set; }
        /// <summary>
        /// 用户可操作的模块集合
        /// </summary>
        public IEnumerable<Module> Modules { get; set; }
        #endregion
    }
}
