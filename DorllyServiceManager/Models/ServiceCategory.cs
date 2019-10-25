using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DorllyServiceManager.Models
{
    public class ServiceCategory
    {
        public int Id { get; set; }
        [StringLength(64)]
        public string Name { get; set; }
        [StringLength(24)]
        public string Code { get; set; }
        public int? ParentId { get; set; }
        public ServiceCategory Parent { get; set; }
        public int Level { get; set; }
        [StringLength(512)]
        public string Remark { get; set; }
        public bool Status { get; set; }
        public virtual ICollection<ServiceCategory> Children { get; set; }
        public virtual ICollection<CategoryProperties> Properties { get; set; }

    }
}
