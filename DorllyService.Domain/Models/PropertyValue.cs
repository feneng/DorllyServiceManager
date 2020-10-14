using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DorllyService.Common.Enums;

namespace DorllyService.Domain
{
    public class PropertyValue
    {
        public int Id { get; set; }
        [StringLength(24)]
        [Required]
        [Display(Name = "属性值ID")]
        public string Code { get; set; }
        [StringLength(64)]
        [Required]
        [Display(Name = "属性值名称")]
        public string Name { get; set; }
        public int PropertyId { get; set; }
        public ServiceProperty Property { get; set; }
        public bool Status { get; set; }

        [NotMapped]
        [Display(Name = "状态")]
        public string StateName => ((StateStatus)(Status?1:0)).ToString();
    }
}
