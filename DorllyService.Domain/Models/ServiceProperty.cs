﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DorllyService.Common.Enums;

namespace DorllyService.Domain
{
    public class ServiceProperty
    {
        public int Id { get; set; }
        [StringLength(24)]
        [Display(Name = "属性编号")]
        public string Code { get; set; }
        [StringLength(64)]
        [Display(Name = "属性名称")]
        public string Name { get; set; }
        public int Type { get; set; }
        public int InputType { get; set; }
        [Display(Name = "是否必填")]
        public bool NotNull { get; set; }
        public byte State { get; set; }

        public virtual ICollection<CategoryProperties> Categories { get; set; }
        [NotMapped]
        [Display(Name = "属性类型")]
        public string TypeName
        {
            get
            {
                return ((PropertyType)Type).ToString();
            }
        }
        [NotMapped]
        [Display(Name = "填写类型")]
        public string InputTypeName
        {
            get
            {
                return ((InputType)InputType).ToString();
            }
        }
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
