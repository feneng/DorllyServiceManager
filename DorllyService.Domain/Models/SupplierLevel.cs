using System.ComponentModel.DataAnnotations;

namespace DorllyService.Domain
{
    public class SupplierLevel
    {
        public int Id { get; set; }
        [StringLength(24)]
        [Display(Name = "编号")]
        public string Code { get; set; }
        [StringLength(64)]
        [Display(Name = "级别名")]
        public string Name { get; set; }
        [Display(Name = "本级积分下限")]
        public int MinScore { get; set; }
        [Display(Name = "本级积分上限")]
        public int MaxScore { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "备注")]
        public string Remark { get; set; }
    }
}
