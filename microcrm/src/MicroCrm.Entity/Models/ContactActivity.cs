using System;
using System.ComponentModel.DataAnnotations;
using URF.Core.EF.Trackable;

namespace MicroCrm.Domain.Models
{
  public partial class ContactActivity : Entity
  {

    [Display(Name = "CompanyId", Description = "CompanyId")]
    public virtual int CompanyId { get; set; }

    [Display(Name = "ContactId", Description = "ContactId")]
    public virtual int ContactId { get; set; }

    [Required]
    [Display(Name = "Name", Description = "Name", Order = 1)]
    [MaxLength(250)]
    public virtual string Name { get; set; }

    [Required]
    [Display(Name = "Brand Machine", Description = "Brand Machine", Order = 2)]
    [MaxLength(250)]
    public virtual string Title { get; set; }

    [Display(Name = "Activity Date", Description = "Activity Date", Order = 3)]
    public virtual DateTime ActivityDate { get; set; } = DateTime.Now;

    [Display(Name = "Channel", Description = "Channel")]
    public virtual int Channel { get; set; }

    [Display(Name = "Content", Description = "Content", Order = 7)]
    [MaxLength(250)]
    public virtual string Content { get; set; }

  }
}
