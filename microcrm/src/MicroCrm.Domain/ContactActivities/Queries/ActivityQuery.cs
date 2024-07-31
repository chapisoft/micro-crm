using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MicroCrm.Application.Shared;
using MicroCrm.Domain.Models;

namespace MicroCrm.Application.ContactActivities.Queries
{
   public  class ActivityPaginationQuery:IRequest<PageResponse<ContactActivity>>
    {
    public int Page { get; set; } = 1;
    public int Rows { get; set; } = 10;
    public string Sort { get; set; } = "Id";
    public string Order { get; set; } = "dsc";
    public string FilterRules { get; set; } = "";
    public ActivityPaginationQuery()
    {

    }
    public ActivityPaginationQuery(int page,
                         int rows,
                         string sort,
                         string order,
                         string filterRules)
    {
      this.Page = page;
      this.Rows = rows;
      this.Sort = sort;
      this.Order = order;
      this.FilterRules = filterRules;
    }
    }

  public class GetActivityById : IRequest<ContactActivity>
  {
    public int Id { get; set; }
    public GetActivityById()
    {

    }
  }
}
