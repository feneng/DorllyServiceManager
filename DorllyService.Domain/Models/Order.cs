using System;
using System.ComponentModel.DataAnnotations;

namespace DorllyService.Domain
{
    public class Order
    {
        public int Id { get; set; }
        [StringLength(48)]
        public string OrderNo { get; set; }
        [StringLength(48)]
        public string Buyer { get; set; }
        [StringLength(36)]
        public string ContactUser { get; set; }
        [StringLength(36)]
        public string ContactPhone { get; set; }
        [StringLength(512)]
        public string BuyerRemark { get; set; }
        [StringLength(256)]
        public string BuyerAddress { get; set; }
        [StringLength(64)]
        public string BuyerCompany { get; set; }
        public int BelongGardenId { get; set; }
        public Garden BelongGarden { get; set; }
        public int SupplierId { get; set; }
        public ServiceSupplier Supplier { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime OrderTime { get; set; }
        public byte State { get; set; }
    }
}
