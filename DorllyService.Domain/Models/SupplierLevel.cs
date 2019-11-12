using System.ComponentModel.DataAnnotations;

namespace DorllyService.Domain
{
    public class SupplierLevel
    {
        public int Id { get; set; }
        [StringLength(24)]
        public string Code { get; set; }
        [StringLength(64)]
        public string Name { get; set; }
        public int MinScore { get; set; }
        public int MaxScore { get; set; }
        [DataType(DataType.Text)]
        public string Remark { get; set; }
    }
}
