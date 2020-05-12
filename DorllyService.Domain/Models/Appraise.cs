using System;
using System.ComponentModel.DataAnnotations;

namespace DorllyService.Domain
{
    public class Appraise
    {
        public int Id { get; set; }
        [Display(Name = "订单编号")]
        public int OrderId { get; set; }
        [Display(Name = "订单编号")]
        public Order Order { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "评价时间")]
        public DateTime AppraiseTime { get; set; }
        [Display(Name = "星级")]
        public int StarClass { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "评价内容")]
        public string Content { get; set; }
        [Display(Name = "匿名")]
        public bool Anonymous { get; set; }
    }
}
