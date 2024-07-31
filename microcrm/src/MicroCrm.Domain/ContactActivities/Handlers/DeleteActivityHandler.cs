using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MicroCrm.Application.ContactActivities.Commands;
using MicroCrm.Service;
using URF.Core.Abstractions;

namespace MicroCrm.Application.ContactActivities.Handlers
{
  public class DeleteActivityHandler : IRequestHandler<DeleteActivityCommand>
  {
    private readonly IUnitOfWork unitOfWork;
    private readonly IContactActivityService activityService;

    public DeleteActivityHandler(
       IUnitOfWork unitOfWork,
      IContactActivityService activityService
      )
    {
      this.unitOfWork = unitOfWork;
      this.activityService = activityService;
    }
    public async Task<Unit> Handle(DeleteActivityCommand request, CancellationToken cancellationToken) {
      var items = await this.activityService.Queryable().Where(x => request.Id.Contains(x.Id)
        ).ToListAsync();
      foreach(var item in items)
      {
        this.activityService.Delete(item);
      }
      await this.unitOfWork.SaveChangesAsync();
      return await Task.FromResult(Unit.Value);
      }
  }
}
