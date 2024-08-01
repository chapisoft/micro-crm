using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using URF.Core.EF.Trackable;
using System.Text;

namespace MicroCrm.Domain.Models
{
  public partial class Customer : URF.Core.EF.Trackable.Entity
  {


    [Display(Name = "Name", Description = "Name")]
    [MaxLength(50)]
    [Required]
    public virtual string Name { get; set; }

    [Display(Name = "Contact", Description = "Contact")]
    [MaxLength(12)]
    //[RegularExpression(@"^[a-zA-Z''-'\s]{1,12}$", ErrorMessage = "Characters are not allowed.")]
    public virtual string Contact { get; set; }
    [Display(Name = "PhoneNumber", Description = "PhoneNumber")]
    [MaxLength(20)]
    //[RegularExpression(@"^1(3|4|5|6|7|8|9)\d{9}$", ErrorMessage = "Enter a valid mobile phone number.")]
    public virtual string PhoneNumber { get; set; }
    [Display(Name = "Address", Description = "Address")]
    [MaxLength(50)]
    public virtual string Address { get; set; }


  }
}
