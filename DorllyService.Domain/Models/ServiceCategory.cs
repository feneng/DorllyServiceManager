using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DorllyService.Domain
{
    public class ServiceCategory
    {
        public int Id { get; set; }
        [StringLength(64)]
        [Display(Name="服务名")]
        public string Name { get; set; }
        [StringLength(24)]
        [Display(Name = "服务代码")]
        public string Code { get; set; }
        [Display(Name = "上级类别")]
        public int? ParentId { get; set; }
        [Display(Name = "上级类别")]
        public ServiceCategory Parent { get; set; }
        [Display(Name = "层级")]
        public int Level { get; set; }
        [StringLength(512)]
        [Display(Name = "备注")]
        public string Remark { get; set; }
        [Display(Name = "状态")]
        public bool Status { get; set; }
        public virtual ICollection<ServiceCategory> Children { get; set; }
        public virtual ICollection<CategoryProperties> Properties { get; set; }

    }
}
