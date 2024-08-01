using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MicroCrm.Application.Projects.Commands;
using MicroCrm.Service;
using URF.Core.Abstractions;

namespace MicroCrm.Application.Projects.Handlers
{
  public class DeleteProjectHandler : IRequestHandler<DeleteProjectCommand>
  {
    private readonly IUnitOfWork unitOfWork;
    private readonly IProjectService projectService;

    public DeleteProjectHandler(
       IUnitOfWork unitOfWork,
      IProjectService projectService
      )
    {
      this.unitOfWork = unitOfWork;
      this.projectService = projectService;
    }
    public async Task<Unit> Handle(DeleteProjectCommand request, CancellationToken cancellationToken) {
      var items = await this.projectService.Queryable().Where(x => request.Id.Contains(x.Id)
        ).ToListAsync();
      foreach(var item in items)
      {
        this.projectService.Delete(item);
      }
      await this.unitOfWork.SaveChangesAsync();
      return await Task.FromResult(Unit.Value);
      }
  }
}
