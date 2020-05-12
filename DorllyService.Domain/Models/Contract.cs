using System;
using System.ComponentModel.DataAnnotations;

namespace DorllyService.Domain
{
    public class Contract
    {
        public int Id { get; set; }
        [StringLength(128)]
        [Display(Name = "名称")]
        public string Name { get; set; }
        [StringLength(48)]
        [Display(Name = "编号")]
        public string ContractNo { get; set; }
        [StringLength(64)]
        [Display(Name = "甲方")]
        public string FirstParty { get; set; }
        [StringLength(64)]
        [Display(Name = "乙方")]
        public string SecondParty { get; set; }
        [Display(Name = "所属园区")]
        public int BelongGardenId { get; set; }
        [Display(Name = "所属园区")]
        public Garden BelongGarden { get; set; }
        [Display(Name = "服务商")]
        public int SupplierId { get; set; }
        [Display(Name = "服务商")]
        public ServiceSupplier Supplier { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "内容")]
        public string Content { get; set; }
        [StringLength(128)]
        [Display(Name = "附件")]
        public string Enclosure { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "签订时间")]
        public DateTime SignDate { get; set; }
        [Display(Name = "状态")]
        public byte State { get; set; }
    }
}
