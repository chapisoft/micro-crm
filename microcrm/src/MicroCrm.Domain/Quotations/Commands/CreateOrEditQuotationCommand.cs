using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MicroCrm.Domain.Models;

namespace MicroCrm.Application.Quotations.Commands
{
  public class CreateOrEditQuotationCommand:IRequest<Quotation>
  {
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public int ContactId { get; set; }
    public string Name { get; set; }
    public string Title { get; set; }
    public string Department { get; set; }
    public string BusinessPhone { get; set; }
    public string Ext { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string LeadTimeToShip { get; set; }
    public string ExpireTime { get; set; }
    public int IncludedVat { get; set; }
    public decimal Amount { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalAmount { get; set; }
    public string Note { get; set; }
    public int Status { get; set; }
    public string ApprovedBy { get; set; }
    public string Money { get; set; }
    public decimal Exchange { get; set; } = 1;
    public virtual DateTime? CreatedDate { get; set; }
    public virtual string CreatedBy { get; set; }
    public virtual DateTime? LastModifiedDate { get; set; }
    public virtual string LastModifiedBy { get; set; }
  }
}
