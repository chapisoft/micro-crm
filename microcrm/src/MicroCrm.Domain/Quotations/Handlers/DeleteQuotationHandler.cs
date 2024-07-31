using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MicroCrm.Application.Quotations.Commands;
using MicroCrm.Service;
using URF.Core.Abstractions;

namespace MicroCrm.Application.Quotations.Handlers
{
  public class DeleteQuotationHandler : IRequestHandler<DeleteQuotationCommand>
  {
    private readonly IUnitOfWork unitOfWork;
    private readonly IQuotationService quotationService;

    public DeleteQuotationHandler(
       IUnitOfWork unitOfWork,
      IQuotationService cuotationService
      )
    {
      this.unitOfWork = unitOfWork;
      this.quotationService = cuotationService;
    }
    public async Task<Unit> Handle(DeleteQuotationCommand request, CancellationToken cancellationToken) {
      var items = await this.quotationService.Queryable().Where(x => request.Id.Contains(x.Id)
        ).ToListAsync();
      foreach(var item in items)
      {
        this.quotationService.Delete(item);
      }
      await this.unitOfWork.SaveChangesAsync();
      return await Task.FromResult(Unit.Value);
      }
  }
}
