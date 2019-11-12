using System;
using System.ComponentModel.DataAnnotations;

namespace DorllyService.Domain
{
    public class ServiceSupplier
    {
        public int Id { get; set; }
        [StringLength(24)]
        public string Code { get; set; }
        [StringLength(64)]
        public string FullName { get; set; }
        [StringLength(32)]
        public string Abbreviation { get; set; }
        public Garden BelongGarden { get; set; }
        [StringLength(128)]
        public string ServiceScope { get; set; }
        public int Level { get; set; }
        [StringLength(64)]
        public string ChargePerson { get; set; }
        [StringLength(48)]
        public string ContactPhone { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }
        [StringLength(64)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [StringLength(64)]
        public string WorkTel { get; set; }
        [StringLength(64)]
        public string SupplierFrom { get; set; }
        [StringLength(128)]
        public string Avatar { get; set; }
        public byte ApproveState { get; set; }
        public byte CooperationState { get; set; }
    }
}
