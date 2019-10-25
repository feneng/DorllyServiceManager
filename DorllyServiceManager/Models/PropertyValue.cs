using System.ComponentModel.DataAnnotations;

namespace DorllyServiceManager.Models
{
    public class PropertyValue
    {
        public int Id { get; set; }
        [StringLength(24)]
        public string Code { get; set; }
        [StringLength(64)]
        public string Name { get; set; }
        //public ServiceProperty Property { get; set; }
        public bool Status { get; set; }
    }
}
