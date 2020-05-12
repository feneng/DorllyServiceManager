using System;
using System.ComponentModel.DataAnnotations;

namespace DorllyService.Domain
{
    public class ServiceSupplier
    {
        public int Id { get; set; }
        [StringLength(24)]
        [Display(Name = "代码")]
        public string Code { get; set; }
        [StringLength(64)]
        [Display(Name = "全称")]
        public string FullName { get; set; }
        [StringLength(32)]
        [Display(Name = "简称")]
        public string Abbreviation { get; set; }
        [Display(Name = "所属园区")]
        public int? BelongGardenId { get; set; }
        [Display(Name = "所属园区")]
        public Garden BelongGarden { get; set; }
        [StringLength(128)]
        [Display(Name = "服务范围")]
        public string ServiceScope { get; set; }
        [Display(Name = "级别")]
        public int Level { get; set; }
        [StringLength(64)]
        [Display(Name = "法人")]
        public string ChargePerson { get; set; }
        [StringLength(48)]
        [Display(Name ="联系电话")]
        public string ContactPhone { get; set; }
        [DataType(DataType.Date)]
        [Display(Name ="创建日期")]
        public DateTime CreateDate { get; set; }
        [StringLength(64)]
        [Display(Name ="邮件地址")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [StringLength(64)]
        [Display(Name ="工作电话")]
        public string WorkTel { get; set; }
        [StringLength(64)]
        [Display(Name ="服务商地址")]
        public string SupplierFrom { get; set; }
        [StringLength(128)]
        [Display(Name ="图标")]
        public string Avatar { get; set; }
        [Display(Name ="审核状态")]
        public byte ApproveState { get; set; }
        [Display(Name ="合作状态")]
        public byte CooperationState { get; set; }
    }
}
