using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MicroCrm.Domain.Models;
using MicroCrm.Application.Quotations.Commands;
using MicroCrm.Service;
using URF.Core.Abstractions;
using Mapster;
namespace MicroCrm.Application.Quotations.Handlers
{
  public class CreateOrEditQuotationHandler : IRequestHandler<CreateOrEditQuotationCommand, Quotation>
  {
    private readonly IUnitOfWork unitOfWork;
    private readonly IQuotationService quotationService;

    public CreateOrEditQuotationHandler(
      IUnitOfWork unitOfWork,
      IQuotationService quotationService
      )
    {
      this.unitOfWork = unitOfWork;
      this.quotationService = quotationService;
    }
    public async Task<Quotation> Handle(CreateOrEditQuotationCommand request, CancellationToken cancellationToken) {
      var quotation = request.Adapt<Quotation>();
      var response =await this.quotationService.CreateOrEdit(quotation);
      await this.unitOfWork.SaveChangesAsync();
      return quotation;

      }
  }
}
