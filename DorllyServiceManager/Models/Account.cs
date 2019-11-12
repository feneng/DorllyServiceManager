using DorllyService.Domain;
using System.Collections.Generic;

namespace DorllyServiceManager.Models
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
        public string LogName { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string PassWord { get; set; }
        /// <summary>
        /// 管理员标识
        /// </summary>
        public string AdminIdentity { get; set; }
        /// <summary>
        /// 用户头像
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// 用户所属系统
        /// </summary>
        public int SystemId { get; set; }
        /// <summary>
        /// 权限集合
        /// </summary>
        public List<Permission> Permissions { get; set; }
        /// <summary>
        /// 角色的集合
        /// </summary>
        public List<Role> Roles { get; set; }
        /// <summary>
        /// 用户可操作的模块集合
        /// </summary>
        public List<Module> Modules { get; set; }
        #endregion
    }
}
