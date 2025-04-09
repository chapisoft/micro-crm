using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MicroCrm.Domain.Models;

namespace MicroCrm.Application.AqDetails.Commands
{
  public class CreateOrEditAqDetailCommand:IRequest<AqDetail>
  {
    public int Id { get; set; }
    public int QaId { get; set; }
    public int ProductId { get; set; }
    public string PartNo { get; set; }
    public string ItemName { get; set; }
    public int Qty { get; set; } = 1;
    public decimal Price { get; set; } = 0;
    public decimal ShipFee { get; set; } = 0;
    public decimal Margin { get; set; } = 0;
    public decimal Discount { get; set; } = 0;
    public decimal Vat { get; set; } = 0;
    public decimal ImportTax { get; set; } = 0;
    public decimal OtherFee { get; set; } = 0;
    public decimal Exchange { get; set; } = 1;
    public decimal Amount { get; set; }
    public string Remark { get; set; }
    public int Type { get; set; }
    public int Subsidiary { get; set; }
    public string NameEn { get; set; }
    public string DescriptionEn { get; set; }
    public virtual string Unit { get; set; }
    public virtual string ImagePath { get; set; }
    public virtual string Description { get; set; }
    public virtual DateTime? CreatedDate { get; set; }
    public virtual string CreatedBy { get; set; }
    public virtual DateTime? LastModifiedDate { get; set; }
    public virtual string LastModifiedBy { get; set; }
  }
}
