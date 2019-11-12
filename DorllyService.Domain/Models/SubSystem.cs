using System.ComponentModel.DataAnnotations;

namespace DorllyService.Domain
{
    public class SubSystem
    {
        public int Id { get; set; }
        [StringLength(64)]
        public string Name { get; set; }
        [StringLength(512)]
        public string Description { get; set; }
        [StringLength(24)]
        public string Code { get; set; }
        public byte State { get; set; }
    }
}
