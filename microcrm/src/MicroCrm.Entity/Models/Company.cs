using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using URF.Core.EF.Trackable;

namespace MicroCrm.Domain.Models
{
  public partial class Company : Entity
  {
    [Display(Name = "Name", Description = "Name")]
    [MaxLength(250)]
    [Required]
    //[Index(IsUnique = true)]
    public virtual string Name { get; set; }

    [Display(Name = "FullName", Description = "FullName")]
    [MaxLength(250)]
    public virtual string FullName { get; set; }

    [Display(Name = "Code", Description = "Code")]
    [MaxLength(12)]
    //[Index(IsUnique = true)]
    [Required]
    public virtual string Code { get; set; }

    [Display(Name = "Address", Description = "Address")]
    [MaxLength(500)]
    public virtual string Address { get; set; }

    [Display(Name = "Contact", Description = "Contact")]
    [MaxLength(250)]
    public virtual string Contact { get; set; }

    [Display(Name = "PhoneNumber", Description = "PhoneNumber")]
    [MaxLength(20)]
    public virtual string PhoneNumber { get; set; }

    [Display(Name = "City", Description = "City")]
    [MaxLength(20)]
    public virtual string PostCode { get; set; }

    [Display(Name = "Note", Description = "Note")]
    [MaxLength(500)]
    public virtual string Note { get; set; }

    [Display(Name = "Register Date", Description = "Register Date")]
    [DefaultValue("now")]
    public virtual DateTime RegisterDate { get; set; }

    [Display(Name = "Private", Description = "Private")]
    public virtual int Private { get; set; }

  }
}
