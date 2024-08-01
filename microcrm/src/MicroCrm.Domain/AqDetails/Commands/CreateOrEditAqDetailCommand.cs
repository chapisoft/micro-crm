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
    public int ProductId { get; set; }
    public string PartNo { get; set; }
    public string ItemName { get; set; }
    public int Qty { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public decimal Vat { get; set; }
    public decimal Amount { get; set; }
    public decimal AmountIncVat { get; set; }
    public string Remark { get; set; }
    public int QaId { get; set; }
    public int Type { get; set; }
  }
}
