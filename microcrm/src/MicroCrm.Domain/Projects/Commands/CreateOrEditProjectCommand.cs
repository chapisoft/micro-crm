using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MicroCrm.Domain.Models;

namespace MicroCrm.Application.Projects.Commands
{
  public class CreateOrEditProjectCommand:IRequest<Project>
  {
    public int Id { get; set; }
    public  string Name { get; set; }
    public  string Contact { get; set; }
    public  string PhoneNumber { get; set; }
    public  string Address { get; set; }
  }
}
