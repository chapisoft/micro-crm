using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroCrm.Dto
{
  public class BaseRequest
  {
    public int? PageIndex { set; get; }
    public int? PageSize { set; get; }
    public int? Id { set; get; }
    public int? CompanyId { set; get; }
    public string CreatedBy { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public int? TenantId { get; set; }
    public string Keyword { set; get; }
  }
}
