using System.ComponentModel.DataAnnotations;

namespace DorllyService.Domain
{
    public class Garden
    {
        public int Id { get; set; }
        [StringLength(64)]
        [Display(Name ="园区名")]
        public string Name { get; set; }
        [StringLength(512)]
        [Display(Name = "园区地址")]
        public string Address { get; set; }
        [Display(Name = "状态")]
        public byte State { get; set; }
    }
}
