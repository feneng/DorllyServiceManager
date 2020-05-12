using System.ComponentModel.DataAnnotations;

namespace DorllyService.Domain
{
    public class SubSystem
    {
        public int Id { get; set; }
        [StringLength(64)]
        [Display(Name = "系统名")]
        public string Name { get; set; }
        [StringLength(512)]
        [Display(Name = "描述")]
        public string Description { get; set; }
        [StringLength(24)]
        [Display(Name = "编码")]
        public string Code { get; set; }
        [Display(Name = "状态")]
        public byte State { get; set; }
    }
}
