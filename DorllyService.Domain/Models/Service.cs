using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DorllyService.Common.Enums;

namespace DorllyService.Domain
{
    public class Service
    {
        public int Id { get; set; }
        [StringLength(30)]
        [Display(Name = "编码")]
        [Required]
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
        [Display(Name ="服务分类")]
        public int CategoryId { get; set; }
        [Display(Name = "服务分类")]
        public ServiceCategory Category { get; set; }
        [Display(Name = "状态")]
        public byte State { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "更新日期")]
        public DateTime UpdateDate { get; set; }
        public int UpdateUserId { get; set; }
        [Display(Name = "修改人")]
        public User UpdateUser { get; set; }

        [NotMapped]
        [Display(Name = "状态")]
        public string StateName
        {
            get
            {
                return ((StateStatus)State).ToString();
            }
        }
    }
}
