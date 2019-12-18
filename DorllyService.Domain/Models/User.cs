using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DorllyService.Domain
{
    public class User
    {
        public int Id { get; set; }
        [StringLength(8)]
        public string Name { get; set; }
        [StringLength(8)]
        public string Account { get; set; }
        [StringLength(64)]
        public string Password { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Birthday { get; set; }
        public byte Sex { get; set; }
        public int? BelongGardenId { get; set; }
        public Garden BelongGarden { get; set; }
        [StringLength(48)]
        public string ContactPhone { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime LastLoginTime { get; set; }
        [StringLength(64)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [StringLength(64)]
        public string WorkTel { get; set; }
        [StringLength(128)]
        public string Avatar { get; set; }
        public byte State { get; set; }
        public int? SupplierId { get; set; }
        [StringLength(8)]
        public string Salt { get; set; }
        public ServiceSupplier Supplier { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
