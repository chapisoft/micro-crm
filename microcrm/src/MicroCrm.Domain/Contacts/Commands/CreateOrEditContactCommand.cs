using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MicroCrm.Domain.Models;

namespace MicroCrm.Application.Contacts.Commands
{
  public class CreateOrEditContactCommand:IRequest<Contact>
  {
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string Name { get; set; }
    public string Title { get; set; }
    public string Department { get; set; }
    public string BusinessPhone { get; set; }
    public string Ext { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string Asignee { get; set; }
  }
}
