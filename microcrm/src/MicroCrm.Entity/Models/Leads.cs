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
    public List<Contact> Contacts { get; set; }
    public List<Project> Projects { get; set; }
    public List<ContactActivity> ContactActivities { get; set; }
    public List<Quotation> Quotations { get; set; }
  }
}
