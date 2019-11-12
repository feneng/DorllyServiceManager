using System.ComponentModel.DataAnnotations;

namespace DorllyService.Domain
{
    public class SystemSetting
    {
        public int Id { get; set; }
        [StringLength(24)]
        public string Code { get; set; }
        [StringLength(512)]
        public byte Value { get; set; }
        [StringLength(24)]
        public string DataType { get; set; }
        public bool Status { get; set; }
        public int SystemId { get; set; }
        public SubSystem System { get; set; }
    }
}
