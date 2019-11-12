using System;
using System.ComponentModel.DataAnnotations;

namespace DorllyService.Domain
{
    public class Appraise
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime AppraiseTime { get; set; }
        public int StarClass { get; set; }
        [DataType(DataType.Text)]
        public string Content { get; set; }
        public bool Anonymous { get; set; }
    }
}
