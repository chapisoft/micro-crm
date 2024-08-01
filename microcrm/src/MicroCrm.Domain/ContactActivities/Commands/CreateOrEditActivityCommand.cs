using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MicroCrm.Domain.Models;

namespace MicroCrm.Application.ContactActivities.Commands
{
  public class CreateOrEditActivityCommand:IRequest<ContactActivity>
  {
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public int ContactId { get; set; }
    public string Name { get; set; }
    public string Title { get; set; }
    public DateTime ActivityDate { get; set; }
    public int Channel { get; set; }
    public string Content { get; set; }
  }
}
