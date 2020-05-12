using System.ComponentModel.DataAnnotations;

namespace DorllyService.Domain
{
    public class SystemSetting
    {
        public int Id { get; set; }
        [StringLength(24)]
        [Display(Name = "编码")]
        public string Code { get; set; }
        [StringLength(512)]
        [Display(Name = "值")]
        public byte Value { get; set; }
        [StringLength(24)]
        [Display(Name = "数据类型")]
        public string DataType { get; set; }
        [Display(Name = "状态")]
        public bool Status { get; set; }
        [Display(Name = "所属系统")]
        public int SystemId { get; set; }
        [Display(Name = "所属系统")]
        public SubSystem System { get; set; }
    }
}
