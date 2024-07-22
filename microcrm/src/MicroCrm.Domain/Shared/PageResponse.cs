using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroCrm.Application.Shared
{
  public class PageResponse<T>
  {
    public int total { get; set; }
    public IEnumerable<T> rows {get;set;}

  }
}
