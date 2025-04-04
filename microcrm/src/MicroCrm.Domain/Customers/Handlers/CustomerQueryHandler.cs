﻿using System;
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
using MicroCrm.Application.Customers.Queries;

namespace MicroCrm.Application.Customers.Handlers
{
  public class CustomerQueryHandler : IRequestHandler<CustomerPaginationQuery, PageResponse<Customer>>
    , IRequestHandler<GetCustomerById, Customer>
  {
    private readonly ICustomerService customerService;

    public CustomerQueryHandler(
      ICustomerService customerService  
      )
    {
      this.customerService = customerService;
    }
    public async Task<PageResponse<Customer>> Handle(CustomerPaginationQuery request, CancellationToken cancellationToken)
    {
      var filters = PredicateBuilder.FromFilter<Customer>(request.FilterRules);
      var total = await this.customerService
                           .Query(filters).CountAsync();
      var pagerows = (await this.customerService
                           .Query(filters)
                         .OrderBy(n => n.OrderBy($"{request.Sort} {request.Order}"))
                         .Skip(request.Page - 1).Take(request.Rows).SelectAsync())
                         .ToList();
      var pagelist = new PageResponse<Customer> { total = total, rows = pagerows };
      return pagelist;
    }

    public async Task<Customer> Handle(GetCustomerById request, CancellationToken cancellationToken) {
      return await this.customerService.FindAsync(request.Id);
    }
  }


}
