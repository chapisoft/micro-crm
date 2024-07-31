using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using URF.Core.EF.Trackable;

namespace MicroCrm.Domain.Models
{
  public partial class Project : Entity
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

    [Display(Name = "Start Date", Description = "Start Date", Order = 3)]
    public virtual DateTime StartDate { get; set; } = DateTime.Now;

    [Display(Name = "Clossing Date", Description = "Clossing Date", Order = 4)]
    public virtual DateTime EndDate { get; set; } = DateTime.Now;

    [Display(Name = "Competitors", Description = "Competitors", Order = 5)]
    [MaxLength(250)]
    public virtual string Competitors { get; set; }

    [Display(Name = "Total Volume", Description = "Total Volume", Order = 6)]
    [DefaultValue(0)]
    public virtual decimal TotalVolume { get; set; } = 0;

    [Display(Name = "Note", Description = "Note", Order = 7)]
    [MaxLength(250)]
    public virtual string Note { get; set; }

    [Display(Name = "Status", Description = "Status", Order = 8)]
    [DefaultValue(0)]
    public virtual int Status { get; set; } = 0;

    [Display(Name = "Approved By", Description = "Approved By", Order = 9)]
    [MaxLength(250)]
    public virtual string ApprovedBy { get; set; }

    [Display(Name = "Details", Description = "Details", Order = 10)]
    public virtual ICollection<AqDetail> Details { get; set; }

  }
}
