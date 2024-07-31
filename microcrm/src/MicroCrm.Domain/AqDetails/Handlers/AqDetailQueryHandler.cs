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
using MicroCrm.Application.AqDetails.Queries;

namespace MicroCrm.Application.AqDetails.Handlers
{
  public class AqDetailQueryHandler : IRequestHandler<AqDetailPaginationQuery, PageResponse<AqDetail>>
    , IRequestHandler<GetAqDetailById, AqDetail>
  {
    private readonly IAqDetailService aqDetailService;

    public AqDetailQueryHandler(
      IAqDetailService aqDetailService  
      )
    {
      this.aqDetailService = aqDetailService;
    }
    public async Task<PageResponse<AqDetail>> Handle(AqDetailPaginationQuery request, CancellationToken cancellationToken)
    {
      var filters = PredicateBuilder.FromFilter<AqDetail>(request.FilterRules);
      var total = await this.aqDetailService
                           .Query(filters).CountAsync();
      var pagerows = (await this.aqDetailService
                           .Query(filters)
                         .OrderBy(n => n.OrderBy($"{request.Sort} {request.Order}"))
                         .Skip(request.Page - 1).Take(request.Rows).SelectAsync())
                         .ToList();
      var pagelist = new PageResponse<AqDetail> { total = total, rows = pagerows };
      return pagelist;
    }

    public async Task<AqDetail> Handle(GetAqDetailById request, CancellationToken cancellationToken) {
      return await this.aqDetailService.FindAsync(request.Id);
    }
  }


}
