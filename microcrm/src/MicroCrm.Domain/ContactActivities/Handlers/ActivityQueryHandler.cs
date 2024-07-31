using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MicroCrm.Domain.Models;
using MicroCrm.Application.Shared;
using MicroCrm.Service;
using System.Linq.Dynamic.Core;
using MicroCrm.Application.ContactActivities.Queries;

namespace MicroCrm.Application.ContactActivities.Handlers
{
  public class ActivityQueryHandler : IRequestHandler<ActivityPaginationQuery, PageResponse<ContactActivity>>
    , IRequestHandler<GetActivityById, ContactActivity>
  {
    private readonly IContactActivityService activityService;

    public ActivityQueryHandler(
      IContactActivityService activityService  
      )
    {
      this.activityService = activityService;
    }
    public async Task<PageResponse<ContactActivity>> Handle(ActivityPaginationQuery request, CancellationToken cancellationToken)
    {
      var filters = PredicateBuilder.FromFilter<ContactActivity>(request.FilterRules);
      var total = await this.activityService
                           .Query(filters).CountAsync();
      var pagerows = (await this.activityService
                           .Query(filters)
                         .OrderBy(n => n.OrderBy($"{request.Sort} {request.Order}"))
                         .Skip(request.Page - 1).Take(request.Rows).SelectAsync())
                         .ToList();
      var pagelist = new PageResponse<ContactActivity> { total = total, rows = pagerows };
      return pagelist;
    }

    public async Task<ContactActivity> Handle(GetActivityById request, CancellationToken cancellationToken) {
      return await this.activityService.FindAsync(request.Id);
    }
  }


}
