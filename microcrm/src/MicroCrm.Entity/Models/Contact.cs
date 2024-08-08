using System.ComponentModel.DataAnnotations;

namespace MicroCrm.Domain.Models
{
  public partial class Contact : URF.Core.EF.Trackable.Entity
  {

    [Display(Name = "CompanyId", Description = "CompanyId")]
    public virtual int CompanyId { get; set; }

    [Display(Name = "Name", Description = "Name")]
    [MaxLength(250)]
    [Required]
    public virtual string Name { get; set; }

    [Display(Name = "Title", Description = "Title")]
    [MaxLength(250)]
    //[RegularExpression(@"^[a-zA-Z''-'\s]{1,12}$", ErrorMessage = "Characters are not allowed.")]
    public virtual string Title { get; set; }

    [Display(Name = "Department", Description = "Department")]
    [MaxLength(250)]
    //[RegularExpression(@"^[a-zA-Z''-'\s]{1,12}$", ErrorMessage = "Characters are not allowed.")]
    public virtual string Department { get; set; }

    [Display(Name = "Business Phone", Description = "Business Phone")]
    [MaxLength(20)]
    //[RegularExpression(@"^1(3|4|5|6|7|8|9)\d{9}$", ErrorMessage = "Enter a valid mobile phone number.")]
    public virtual string BusinessPhone { get; set; }

    [Display(Name = "Ext", Description = "Ext")]
    [MaxLength(20)]
    //[RegularExpression(@"^1(3|4|5|6|7|8|9)\d{9}$", ErrorMessage = "Enter a valid mobile phone number.")]
    public virtual string Ext { get; set; }

    [Display(Name = "Phone", Description = "Phone")]
    [MaxLength(20)]
    //[RegularExpression(@"^1(3|4|5|6|7|8|9)\d{9}$", ErrorMessage = "Enter a valid mobile phone number.")]
    public virtual string Phone { get; set; }

    [Display(Name = "Email", Description = "Email")]
    [MaxLength(250)]
    public virtual string Email { get; set; }

    [Display(Name = "Address", Description = "Address")]
    [MaxLength(250)]
    public virtual string Address { get; set; }

    [Display(Name = "Asignee", Description = "Asignee")]
    [MaxLength(250)]
    public virtual string Asignee { get; set; }

    [Display(Name = "Source", Description = "Source")]
    [MaxLength(250)]
    public virtual int? Source { get; set; }
  }
}
