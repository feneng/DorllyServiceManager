using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DorllyService.Domain
{
    public class User
    {
        public int Id { get; set; }
        [StringLength(12)]
        [Display(Name = "昵称")]
        public string Name { get; set; }
        [StringLength(12)]
        [Display(Name = "账号")]
        [Required]
        public string Account { get; set; }
        [StringLength(64)]
        public string Password { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "生日")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Birthday { get; set; }
        [Display(Name = "性别")]
        public byte Sex { get; set; }
        [Display(Name = "所属园区")]
        public int? BelongGardenId { get; set; }
        [Display(Name = "所属园区")]
        public Garden BelongGarden { get; set; }
        [StringLength(48)]
        [Display(Name = "联系电话")]
        public string ContactPhone { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "创建时间")]
        public DateTime CreateDate { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "上次登录时间")]
        public DateTime LastLoginTime { get; set; }
        [StringLength(64)]
        [Display(Name = "邮箱")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [StringLength(64)]
        [Display(Name = "工作电话")]
        public string WorkTel { get; set; }
        [StringLength(128)]
        [Display(Name = "头像")]
        public string Avatar { get; set; }
        [Display(Name = "状态")]
        public byte State { get; set; }
        public int? SupplierId { get; set; }
        public ServiceSupplier Supplier { get; set; }
        [StringLength(8)]
        public string Salt { get; set; }
        [Display(Name = "用户类型")]
        public byte UserType { get; set; }
        [Display(Name = "此用户拥有的角色")]
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
