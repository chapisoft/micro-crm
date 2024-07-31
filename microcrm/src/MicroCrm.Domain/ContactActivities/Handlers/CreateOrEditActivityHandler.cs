using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MicroCrm.Domain.Models;
using MicroCrm.Application.ContactActivities.Commands;
using MicroCrm.Service;
using URF.Core.Abstractions;
using Mapster;
namespace MicroCrm.Application.ContactActivities.Handlers
{
  public class CreateOrEditActivityHandler : IRequestHandler<CreateOrEditActivityCommand, ContactActivity>
  {
    private readonly IUnitOfWork unitOfWork;
    private readonly IContactActivityService activityService;

    public CreateOrEditActivityHandler(
      IUnitOfWork unitOfWork,
      IContactActivityService activityService
      )
    {
      this.unitOfWork = unitOfWork;
      this.activityService = activityService;
    }
    public async Task<ContactActivity> Handle(CreateOrEditActivityCommand request, CancellationToken cancellationToken) {
      var activity = request.Adapt<ContactActivity>();
      var response =await this.activityService.CreateOrEdit(activity);
      await this.unitOfWork.SaveChangesAsync();
      return activity;

      }
  }
}
