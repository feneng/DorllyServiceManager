using System;
using System.ComponentModel.DataAnnotations;

namespace DorllyService.Domain
{
    public class Service
    {
        public int Id { get; set; }
        [StringLength(30)]
        [Display(Name = "编码")]
        public string Code { get; set; }
        [StringLength(64)]
        [Display(Name = "服务名")]
        public string Name { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "描述")]
        public string Description { get; set; }
        //public int TopCategoryId { get; set; }
        //public ServiceCategory TopCategory { get; set; }
        //public int MiddleCategoryId { get; set; }
        //public ServiceCategory MiddleCategory { get; set; }
        public int CategoryId { get; set; }
        public ServiceCategory Category { get; set; }
        [Display(Name = "状态")]
        public byte State { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "更新日期")]
        public DateTime UpdateDate { get; set; }
        public int UpdateUserId { get; set; }
        public User UpdateUser { get; set; }
    }
}
