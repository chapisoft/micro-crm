using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MicroCrm.Application.AqDetails.Commands;
using MicroCrm.Service;
using URF.Core.Abstractions;

namespace MicroCrm.Application.AqDetails.Handlers
{
  public class DeleteAqDetailHandler : IRequestHandler<DeleteAqDetailCommand>
  {
    private readonly IUnitOfWork unitOfWork;
    private readonly IAqDetailService aqDetailService;

    public DeleteAqDetailHandler(
       IUnitOfWork unitOfWork,
      IAqDetailService aqDetailService
      )
    {
      this.unitOfWork = unitOfWork;
      this.aqDetailService = aqDetailService;
    }
    public async Task<Unit> Handle(DeleteAqDetailCommand request, CancellationToken cancellationToken) {
      var items = await this.aqDetailService.Queryable().Where(x => request.Id.Contains(x.Id)
        ).ToListAsync();
      foreach(var item in items)
      {
        this.aqDetailService.Delete(item);
      }
      await this.unitOfWork.SaveChangesAsync();
      return await Task.FromResult(Unit.Value);
      }
  }
}
