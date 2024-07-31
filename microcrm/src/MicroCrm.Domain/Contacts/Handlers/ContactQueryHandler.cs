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
using MicroCrm.Application.Contacts.Queries;

namespace MicroCrm.Application.Contacts.Handlers
{
  public class ContactQueryHandler : IRequestHandler<ContactPaginationQuery, PageResponse<Contact>>
    , IRequestHandler<GetContactById, Contact>
  {
    private readonly IContactService contactService;

    public ContactQueryHandler(
      IContactService contactService  
      )
    {
      this.contactService = contactService;
    }
    public async Task<PageResponse<Contact>> Handle(ContactPaginationQuery request, CancellationToken cancellationToken)
    {
      var filters = PredicateBuilder.FromFilter<Contact>(request.FilterRules);
      var total = await this.contactService
                           .Query(filters).CountAsync();
      var pagerows = (await this.contactService
                           .Query(filters)
                         .OrderBy(n => n.OrderBy($"{request.Sort} {request.Order}"))
                         .Skip(request.Page - 1).Take(request.Rows).SelectAsync())
                         .ToList();
      var pagelist = new PageResponse<Contact> { total = total, rows = pagerows };
      return pagelist;
    }

    public async Task<Contact> Handle(GetContactById request, CancellationToken cancellationToken) {
      return await this.contactService.FindAsync(request.Id);
    }
  }


}
