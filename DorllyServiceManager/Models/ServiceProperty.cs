using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DorllyServiceManager.Models
{
    public class ServiceProperty
    {
        public int Id { get; set; }
        [StringLength(24)]
        public string Code { get; set; }
        [StringLength(64)]
        public string Name { get; set; }
        public int Type { get; set; }
        public int InputType { get; set; }
        public bool NotNull { get; set; }
        public byte State { get; set; }
        public virtual ICollection<CategoryProperties> Categories { get; set; }
    }
}
