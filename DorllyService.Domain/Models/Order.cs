using System;
using System.ComponentModel.DataAnnotations;

namespace DorllyService.Domain
{
    public class Order
    {
        public int Id { get; set; }
        [StringLength(48)]
        [Display(Name = "单号")]
        public string OrderNo { get; set; }
        [StringLength(48)]
        [Display(Name = "买家")]
        public string Buyer { get; set; }
        [StringLength(36)]
        [Display(Name = "联系人")]
        public string ContactUser { get; set; }
        [StringLength(36)]
        [Display(Name = "联系电话")]
        public string ContactPhone { get; set; }
        [StringLength(512)]
        [Display(Name = "买方备注")]
        public string BuyerRemark { get; set; }
        [StringLength(256)]
        [Display(Name = "买方地址")]
        public string BuyerAddress { get; set; }
        [StringLength(64)]
        [Display(Name = "买家所属公司")]
        public string BuyerCompany { get; set; }
        public int BelongGardenId { get; set; }
        [Display(Name = "所属园区")]
        public Garden BelongGarden { get; set; }
        public int SupplierId { get; set; }
        [Display(Name = "供应商")]
        public ServiceSupplier Supplier { get; set; }
        public int ServiceId { get; set; }
        [Display(Name = "服务")]
        public Service Service { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "交易时间")]
        public DateTime OrderTime { get; set; }
        [Display(Name = "状态")]
        public byte State { get; set; }
    }
}
