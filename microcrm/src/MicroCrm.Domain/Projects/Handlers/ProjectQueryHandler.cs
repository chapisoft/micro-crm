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
using MicroCrm.Application.Projects.Queries;

namespace MicroCrm.Application.Projects.Handlers
{
  public class ProjectQueryHandler : IRequestHandler<ProjectPaginationQuery, PageResponse<Project>>
    , IRequestHandler<GetProjectById, Project>
  {
    private readonly IProjectService projectService;

    public ProjectQueryHandler(
      IProjectService projectService  
      )
    {
      this.projectService = projectService;
    }
    public async Task<PageResponse<Project>> Handle(ProjectPaginationQuery request, CancellationToken cancellationToken)
    {
      var filters = PredicateBuilder.FromFilter<Project>(request.FilterRules);
      var total = await this.projectService
                           .Query(filters).CountAsync();
      var pagerows = (await this.projectService
                           .Query(filters)
                         .OrderBy(n => n.OrderBy($"{request.Sort} {request.Order}"))
                         .Skip(request.Page - 1).Take(request.Rows).SelectAsync())
                         .ToList();
      var pagelist = new PageResponse<Project> { total = total, rows = pagerows };
      return pagelist;
    }

    public async Task<Project> Handle(GetProjectById request, CancellationToken cancellationToken) {
      return await this.projectService.FindAsync(request.Id);
    }
  }


}
