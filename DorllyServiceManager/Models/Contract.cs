using System;
using System.ComponentModel.DataAnnotations;

namespace DorllyServiceManager.Models
{
    public class Contract
    {
        public int Id { get; set; }
        [StringLength(128)]
        public string Name { get; set; }
        [StringLength(48)]
        public string ContractNo { get; set; }
        [StringLength(64)]
        public string FirstParty { get; set; }
        [StringLength(64)]
        public string SecondParty { get; set; }
        public int BelongGardenId { get; set; }
        public Garden BelongGarden { get; set; }
        public int SupplierId { get; set; }
        public ServiceSupplier Supplier { get; set; }
        [DataType(DataType.Text)]
        public string Content { get; set; }
        [StringLength(128)]
        public string Enclosure { get; set; }
        [DataType(DataType.Date)]
        public DateTime SignDate { get; set; }
        public byte State { get; set; }
    }
}
