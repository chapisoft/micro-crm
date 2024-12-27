using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using URF.Core.EF.Trackable;

namespace MicroCrm.Domain.Models
{
  public partial class Quotation : Entity
  {

    [Display(Name = "CompanyId", Description = "CompanyId")]
    public virtual int CompanyId { get; set; }

    [Display(Name = "ContactId", Description = "ContactId")]
    public virtual int ContactId { get; set; }

    [Required]
    [Display(Name = "Name", Description = "Name", Order = 1)]
    [MaxLength(250)]
    public virtual string Name { get; set; }

    [Display(Name = "Title", Description = "Title", Order = 2)]
    [MaxLength(250)]
    public virtual string Title { get; set; }

    [Display(Name = "Department", Description = "Department", Order = 3)]
    [MaxLength(250)]
    public virtual string Department { get; set; }

    [Display(Name = "Business Phone", Description = "Business Phone", Order = 4)]
    [MaxLength(20)]
    public virtual string BusinessPhone { get; set; }

    [Display(Name = "Ext", Description = "Ext", Order = 5)]
    [MaxLength(20)]
    public virtual string Ext { get; set; }

    [Display(Name = "Phone", Description = "Phone", Order = 6)]
    [MaxLength(20)]
    public virtual string Phone { get; set; }

    [Display(Name = "Email", Description = "Email", Order = 7)]
    [MaxLength(250)]
    public virtual string Email { get; set; }

    [Display(Name = "Effect Date", Description = "Effect Date", Order = 8)]
    public virtual DateTime StartDate { get; set; } = DateTime.Now;

    [Display(Name = "Expired Date", Description = "Expired Date", Order = 9)]
    public virtual DateTime EndDate { get; set; } = DateTime.Now;

    [Display(Name = "Lead Time To Ship", Description = "Lead Time To Ship", Order = 11)]
    [MaxLength(250)]
    public virtual string LeadTimeToShip { get; set; } = "10 - 12 tuần";

    [Display(Name = "Expire Time", Description = "Expire Time", Order = 11)]
    [MaxLength(250)]
    public virtual string ExpireTime { get; set; } = "12 tháng";

    [Display(Name = "Included Vat", Description = "Included Vat", Order = 11)]
    public virtual int IncludedVat { get; set; }

    [Display(Name = "Amount", Description = "Amount", Order = 10)]
    [DefaultValue(0)]
    public virtual decimal Amount { get; set; } = 0;

    [Display(Name = "Discount", Description = "Discount", Order = 10)]
    [DefaultValue(0)]
    public virtual decimal Discount { get; set; } = 0;

    [Display(Name = "TotalAmount", Description = "TotalAmount", Order = 10)]
    [DefaultValue(0)]
    public virtual decimal TotalAmount { get; set; } = 0;

    [Required(ErrorMessage = "Required")]
    [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
    [Display(Name = "Exchange", Description = "Exchange")]
    public decimal Exchange { get; set; } = 1;

    [Display(Name = "Money", Description = "Money")]
    [MaxLength(30)]
    [StringLength(20)]
    public virtual string Money { get; set; }

    [Display(Name = "Note", Description = "Note", Order = 11)]
    [MaxLength(250)]
    public virtual string Note { get; set; }

    [Display(Name = "Status", Description = "Status", Order = 12)]
    [DefaultValue(0)]
    public virtual int Status { get; set; } = 0;

    [Display(Name = "Approved By", Description = "Approved By", Order = 13)]
    [MaxLength(250)]
    public virtual string ApprovedBy { get; set; }
  }
}
