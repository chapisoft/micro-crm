using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace MicroCrm.Application.Quotations.Commands
{
  public class DeleteQuotationCommand:IRequest
  {
    public int[] Id { get; set; }
  }
}
