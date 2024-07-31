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
    public  string Name { get; set; }
    public  string Contact { get; set; }
    public  string PhoneNumber { get; set; }
    public  string Address { get; set; }
  }
}
