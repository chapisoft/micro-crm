using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroCrm.Domain.Models;

namespace MicroCrm.WebUI.Data.Models
{
  public class PrintQuotation
  {
    public Tenant Tenant { get; set; }
    public Company Company { get; set; }
    public Quotation Info { get; set; }
    public List<QuotationItem> Details { get; set; }
  }

  public class QuotationItem
  {
    public string PartNo { get; set; }
    public string ItemName { get; set; }
    public int Qty { get; set; }
    public decimal Price { get; set; }
    public decimal ShipFee { get; set; }
    public decimal Margin { get; set; }
    public decimal Discount { get; set; }
    public decimal Vat { get; set; }
    public decimal ImportTax { get; set; }
    public decimal OtherFee { get; set; }
    public decimal Exchange { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
  }
}
