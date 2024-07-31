using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace MicroCrm.Application.ContactActivities.Commands
{
  public class DeleteActivityCommand:IRequest
  {
    public int[] Id { get; set; }
  }
}
