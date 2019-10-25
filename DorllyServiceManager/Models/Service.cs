using System;
using System.ComponentModel.DataAnnotations;

namespace DorllyServiceManager.Models
{
    public class Service
    {
        public int Id { get; set; }
        [StringLength(30)]
        public string Code { get; set; }
        [StringLength(64)]
        public string Name { get; set; }
        [DataType(DataType.Text)]
        public string Description { get; set; }
        //public int TopCategoryId { get; set; }
        //public ServiceCategory TopCategory { get; set; }
        //public int MiddleCategoryId { get; set; }
        //public ServiceCategory MiddleCategory { get; set; }
        public int CategoryId { get; set; }
        public ServiceCategory Category { get; set; }
        public byte State { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime UpdateDate { get; set; }
        public int UpdateUserId { get; set; }
        public User UpdateUser { get; set; }
    }
}
