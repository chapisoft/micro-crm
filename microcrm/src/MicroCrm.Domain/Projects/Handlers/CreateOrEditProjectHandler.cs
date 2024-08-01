using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MicroCrm.Domain.Models;
using MicroCrm.Application.Projects.Commands;
using MicroCrm.Service;
using URF.Core.Abstractions;
using Mapster;
namespace MicroCrm.Application.Projects.Handlers
{
  public class CreateOrEditProjectHandler : IRequestHandler<CreateOrEditProjectCommand, Project>
  {
    private readonly IUnitOfWork unitOfWork;
    private readonly IProjectService projectService;

    public CreateOrEditProjectHandler(
      IUnitOfWork unitOfWork,
      IProjectService projectService
      )
    {
      this.unitOfWork = unitOfWork;
      this.projectService = projectService;
    }
    public async Task<Project> Handle(CreateOrEditProjectCommand request, CancellationToken cancellationToken) {
      var project = request.Adapt<Project>();
      var response =await this.projectService.CreateOrEdit(project);
      await this.unitOfWork.SaveChangesAsync();
      return project;

      }
  }
}
