using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using URF.Core.EF.Trackable;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroCrm.Domain.Models
{
  public partial class OrderDetail : Entity
  {
    [Required(ErrorMessage = "Required")]
    [Display(Name = "Product", Description = "Product")]
    public virtual int ProductId { get; set; }
    [ForeignKey("ProductId")]
    [Display(Name = "Product", Description = "Product")]
    public Product Product { get; set; }
    [Required(ErrorMessage = "Required")]
    [Range(1, 9999)]
    [DefaultValue(1)]
    [Display(Name = "Qty", Description = "Requirement Qty")]
    public virtual int Qty { get; set; }
    [Required(ErrorMessage = "Required")]
    [Range(1, 9999)]
    [Display(Name = "Price", Description = "Price")]
    [Column(TypeName = "decimal(18, 2)")]
    public virtual decimal Price { get; set; }
    [Required(ErrorMessage = "Required")]
    [Range(1, 9999)]
    [Display(Name = "Amount", Description = "Amount(QtyxPrice)")]
    [Column(TypeName = "decimal(18, 2)")]
    public virtual decimal Amount { get; set; }
    [Display(Name = "Remark", Description = "Remark")]
    [MaxLength(30)]
    [StringLength(20)]
    public virtual string Remark { get; set; }
    [Display(Name = "OrderId", Description = "OrderId")]
    public virtual int OrderId { get; set; }
    //关联OrderId表头
    [ForeignKey("OrderId")]
    [Display(Name = "OrderNo", Description = "OrderNo")]
    public virtual Order Order { get; set; }

  }
}
