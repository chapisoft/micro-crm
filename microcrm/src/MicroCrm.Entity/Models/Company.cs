using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using URF.Core.EF.Trackable;
using System.Text;

namespace MicroCrm.Domain.Models
{
    public partial class Company : URF.Core.EF.Trackable.Entity
    {
        [Display(Name = "Name", Description = "Name")]
        [MaxLength(50)]
        [Required]
        //[Index(IsUnique = true)]
        public virtual string Name { get; set; }
        [Display(Name = "Code", Description = "Code")]
        [MaxLength(12)]
        //[Index(IsUnique = true)]
        [Required]
        public virtual string Code { get; set; }
        [Display(Name = "Address", Description = "Address")]
        [MaxLength(128)]
        [DefaultValue("-")]
        public virtual string Address { get; set; }
        [Display(Name = "Contact", Description = "Contact")]
        [MaxLength(12)]
        public virtual string Contact { get; set; }
        [Display(Name = "PhoneNumber", Description = "PhoneNumber")]
        [MaxLength(20)]
        public virtual string PhoneNumber { get; set; }
        [Display(Name = "注册日期", Description = "注册日期")]
        [DefaultValue("now")]
        public virtual  DateTime RegisterDate { get; set; }
    }
}
