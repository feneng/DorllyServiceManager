using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DorllyServiceManager.Models
{
    public class Module
    {
        public int Id { get; set; }
        [StringLength(64)]
        public string Name { get; set; }
        public byte Type { get; set; }
        [StringLength(128)]
        public string Path { get; set; }
        public int Order { get; set; }
        [StringLength(64)]
        public string Icon { get; set; }
        public int Level { get; set; }
        public int BelongSystemId { get; set; }
        public SubSystem BelongSystem { get; set; }
        public int? ParentId { get; set; }
        public Module Parent { get; set; }
        public bool Status { get; set; }
        public virtual ICollection<Module> Children { get; set; }
    }
}
