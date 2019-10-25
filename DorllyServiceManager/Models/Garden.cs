using System.ComponentModel.DataAnnotations;

namespace DorllyServiceManager.Models
{
    public class Garden
    {
        public int Id { get; set; }
        [StringLength(64)]
        public string Name { get; set; }
        [StringLength(512)]
        public string Address { get; set; }
        public byte State { get; set; }
    }
}
