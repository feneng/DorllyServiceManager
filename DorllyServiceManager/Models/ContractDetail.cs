using System;
using System.ComponentModel.DataAnnotations;

namespace DorllyServiceManager.Models
{
    public class ContractDetail
    {
        public int Id { get; set; }
        public Contract Contract { get; set; }
        public int TopCategoryId { get; set; }
        public ServiceCategory TopCategory { get; set; }
        public int MiddleCategoryId { get; set; }
        public ServiceCategory MiddleCategory { get; set; }
        public int SmallCategoryId { get; set; }
        public ServiceCategory SmallCategory { get; set; }
        public int SupplierId { get; set; }
        public ServiceSupplier Supplier { get; set; }
        public byte SettlementInterval { get; set; }
        public decimal ShareProfit { get; set; }
        public byte SettlementDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime CooperationStartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime CooperationEndDate { get; set; }
    }
}
