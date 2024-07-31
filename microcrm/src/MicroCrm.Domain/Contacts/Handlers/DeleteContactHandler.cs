using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MicroCrm.Application.Contacts.Commands;
using MicroCrm.Service;
using URF.Core.Abstractions;

namespace MicroCrm.Application.Contacts.Handlers
{
  public class DeleteContactHandler : IRequestHandler<DeleteContactCommand>
  {
    private readonly IUnitOfWork unitOfWork;
    private readonly IContactService contactService;

    public DeleteContactHandler(
       IUnitOfWork unitOfWork,
      IContactService contactService
      )
    {
      this.unitOfWork = unitOfWork;
      this.contactService = contactService;
    }
    public async Task<Unit> Handle(DeleteContactCommand request, CancellationToken cancellationToken) {
      var items = await this.contactService.Queryable().Where(x => request.Id.Contains(x.Id)
        ).ToListAsync();
      foreach(var item in items)
      {
        this.contactService.Delete(item);
      }
      await this.unitOfWork.SaveChangesAsync();
      return await Task.FromResult(Unit.Value);
      }
  }
}
