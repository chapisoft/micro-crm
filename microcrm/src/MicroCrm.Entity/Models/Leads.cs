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
    public Company Customer { get; set; }
  }
}
