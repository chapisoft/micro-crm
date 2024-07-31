using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MicroCrm.Domain.Models;
using MicroCrm.Application.AqDetails.Commands;
using MicroCrm.Service;
using URF.Core.Abstractions;
using Mapster;
namespace MicroCrm.Application.AqDetails.Handlers
{
  public class CreateOrEditAqDetailHandler : IRequestHandler<CreateOrEditAqDetailCommand, AqDetail>
  {
    private readonly IUnitOfWork unitOfWork;
    private readonly IAqDetailService aqDetailService;

    public CreateOrEditAqDetailHandler(
      IUnitOfWork unitOfWork,
      IAqDetailService aqDetailService
      )
    {
      this.unitOfWork = unitOfWork;
      this.aqDetailService = aqDetailService;
    }
    public async Task<AqDetail> Handle(CreateOrEditAqDetailCommand request, CancellationToken cancellationToken) {
      var aqDetail = request.Adapt<AqDetail>();
      var response =await this.aqDetailService.CreateOrEdit(aqDetail);
      await this.unitOfWork.SaveChangesAsync();
      return aqDetail;

      }
  }
}
