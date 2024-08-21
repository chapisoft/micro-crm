using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using URF.Core.EF.Trackable;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroCrm.Domain.Models
{
  public partial class AqDetail : Entity
  {

    [Display(Name = "QaId", Description = "QaId")]
    public virtual int QaId { get; set; }
    [Display(Name = "Product", Description = "Product")]
    public virtual int ProductId { get; set; }

    //[ForeignKey("ProductId")]
    //[Display(Name = "Product", Description = "Product")]
    //public Product Product { get; set; }

    [Display(Name = "PartNo", Description = "PartNo")]
    [MaxLength(20)]
    [StringLength(20)]
    public virtual string PartNo { get; set; }

    [Display(Name = "Item Name", Description = "Item Name")]
    [MaxLength(250)]
    [StringLength(250)]
    public virtual string ItemName { get; set; }

    [Required(ErrorMessage = "Required")]
    [Range(1, 9999)]
    [DefaultValue(1)]
    [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
    [Display(Name = "Qty", Description = "Requirement Qty")]
    public virtual int Qty { get; set; } = 1;

    [Required(ErrorMessage = "Required")]
    [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
    [Display(Name = "Price", Description = "Price")]
    [Column(TypeName = "decimal(18, 2)")]
    public virtual decimal Price { get; set; }

    [Required(ErrorMessage = "Required")]
    [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
    [Display(Name = "ShipFee", Description = "ShipFee")]
    [Column(TypeName = "decimal(18, 2)")]
    public virtual decimal ShipFee { get; set; }

    [Required(ErrorMessage = "Required")]
    [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
    [Display(Name = "Margin", Description = "Margin")]
    [Column(TypeName = "decimal(18, 2)")]
    public virtual decimal Margin { get; set; }

    [Required(ErrorMessage = "Required")]
    [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
    [Display(Name = "Discount", Description = "Discount")]
    [Column(TypeName = "decimal(18, 2)")]
    public virtual decimal Discount { get; set; }

    [Required(ErrorMessage = "Required")]
    [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
    [Display(Name = "Vat", Description = "Vat")]
    [Column(TypeName = "decimal(18, 2)")]
    public virtual decimal Vat { get; set; }

    [Required(ErrorMessage = "Required")]
    [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
    [Display(Name = "Import Tax", Description = "Import Tax")]
    [Column(TypeName = "decimal(18, 2)")]
    public virtual decimal ImportTax { get; set; }

    [Required(ErrorMessage = "Required")]
    [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
    [Display(Name = "Other Fee", Description = "Other Fee")]
    [Column(TypeName = "decimal(18, 2)")]
    public virtual decimal OtherFee { get; set; }

    [Required(ErrorMessage = "Required")]
    [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
    [Display(Name = "Exchange", Description = "Exchange")]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Exchange { get; set; } = 1;

    [Required(ErrorMessage = "Required")]
    [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
    [Display(Name = "Amount", Description = "Amount(QtyxPrice)")]
    [Column(TypeName = "decimal(18, 2)")]
    public virtual decimal Amount { get; set; }

    [Display(Name = "Remark", Description = "Remark")]
    [MaxLength(30)]
    [StringLength(20)]
    public virtual string Remark { get; set; }

    [Display(Name = "Type", Description = "Type")]
    public virtual int Type { get; set; }

    [Display(Name = "Subsidiary", Description = "Subsidiary")]
    public virtual int Subsidiary { get; set; }

    ////关联OrderId表头
    //[ForeignKey("OrderId")]
    //[Display(Name = "OrderNo", Description = "OrderNo")]
    //public virtual Order Order { get; set; }

  }
}
