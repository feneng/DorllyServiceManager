using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DorllyService.Domain
{
    public class Module
    {
        public int Id { get; set; }
        [Display(Name ="菜单名")]
        [StringLength(64)]
        public string Name { get; set; }
        [Display(Name = "类型")]
        public byte Type { get; set; }
        [Display(Name = "路径")]
        [StringLength(128)]
        public string Path { get; set; }
        [Display(Name = "排序")]
        public int Order { get; set; }
        [StringLength(64)]
        public string Icon { get; set; }
        public int Level { get; set; }
        public int BelongSystemId { get; set; }
        public SubSystem BelongSystem { get; set; }
        public int? ParentId { get; set; }
        public Module Parent { get; set; }
        [Display(Name = "状态")]
        public bool Status { get; set; }
        public virtual ICollection<Module> Children { get; set; }
    }
}
