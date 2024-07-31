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
using MicroCrm.Application.Quotations.Queries;

namespace MicroCrm.Application.Quotations.Handlers
{
  public class QuotationQueryHandler : IRequestHandler<QuotationPaginationQuery, PageResponse<Quotation>>
    , IRequestHandler<GetQuotationById, Quotation>
  {
    private readonly IQuotationService QuotationService;

    public QuotationQueryHandler(
      IQuotationService QuotationService  
      )
    {
      this.QuotationService = QuotationService;
    }
    public async Task<PageResponse<Quotation>> Handle(QuotationPaginationQuery request, CancellationToken cancellationToken)
    {
      var filters = PredicateBuilder.FromFilter<Quotation>(request.FilterRules);
      var total = await this.QuotationService
                           .Query(filters).CountAsync();
      var pagerows = (await this.QuotationService
                           .Query(filters)
                         .OrderBy(n => n.OrderBy($"{request.Sort} {request.Order}"))
                         .Skip(request.Page - 1).Take(request.Rows).SelectAsync())
                         .ToList();
      var pagelist = new PageResponse<Quotation> { total = total, rows = pagerows };
      return pagelist;
    }

    public async Task<Quotation> Handle(GetQuotationById request, CancellationToken cancellationToken) {
      return await this.QuotationService.FindAsync(request.Id);
    }
  }


}
