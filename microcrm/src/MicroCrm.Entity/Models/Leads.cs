using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroCrm.Domain.Models
{
  public partial class Leads
  {
    public Company Company { get; set; }
    public Contact Contact { get; set; }
    public Project Project { get; set; }
    public ContactActivity ContactActivity { get; set; }
    public Quotation Quotation { get; set; }
  }
}
