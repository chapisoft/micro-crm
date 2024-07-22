using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using URF.Core.EF.Trackable;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroCrm.Domain.Models
{
  public partial class Order : Entity
  {
    public Order()
    {
      OrderDetails = new HashSet<OrderDetail>();
    }
    [Required]
    [Display(Name = "OrderNo", Description = "OrderNo", Order = 1)]
    [MaxLength(8)]
    [MinLength(8)]
    public virtual string OrderNo { get; set; }
    [Display(Name = "Status", Description = "Status", Order = 1)]
    [MaxLength(8)]
    [DefaultValue("Draft")]
    public virtual string Status { get; set; } = "Draft";
    [Display(Name = "OrderDate", Description = "OrderDate", Order = 2)]
    public virtual  DateTime OrderDate { get; set; } = DateTime.Now;
    [Display(Name = "Customer", Description = "Customer", Order = 3)]
    public virtual  int CustomerId { get; set; }
    [Display(Name = "Customer", Description = "Customer", Order = 3)]
    [ForeignKey("CustomerId")]
    public virtual Customer Customer { get; set; }
    [Required]
    [Display(Name = "ShippingAddress", Description = "ShippingAddress", Order = 4)]
    [MaxLength(256)]
    public virtual string ShippingAddress { get; set; }
    [Display(Name = "Contact", Description = "Contact", Order = 5)]
    [MaxLength(20)]
    public virtual string Contact { get; set; }
    [Display(Name = "PhoneNumber", Description = "PhoneNumber")]
    [MaxLength(30)]
    public virtual string PhoneNumber { get; set; }
    [Display(Name = "Remark", Description = "Remark", Order = 6)]
    [MaxLength(100)]
    public virtual string Remark { get; set; }
    [Display(Name = "OrderDetails", Description = "OrderDetails", Order = 6)]
    [InverseProperty("Order")]
    //关联OrderDetails 1-*
    public virtual ICollection<OrderDetail> OrderDetails { get; set; }
  }
}
