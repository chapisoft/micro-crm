using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MicroCrm.Domain.Models;
using MicroCrm.Application.Contacts.Commands;
using MicroCrm.Service;
using URF.Core.Abstractions;
using Mapster;
namespace MicroCrm.Application.Contacts.Handlers
{
  public class CreateOrEditContactHandler : IRequestHandler<CreateOrEditContactCommand, Contact>
  {
    private readonly IUnitOfWork unitOfWork;
    private readonly IContactService contactService;

    public CreateOrEditContactHandler(
      IUnitOfWork unitOfWork,
      IContactService contactService
      )
    {
      this.unitOfWork = unitOfWork;
      this.contactService = contactService;
    }
    public async Task<Contact> Handle(CreateOrEditContactCommand request, CancellationToken cancellationToken) {
      var contact = request.Adapt<Contact>();
      var response =await this.contactService.CreateOrEdit(contact);
      await this.unitOfWork.SaveChangesAsync();
      return contact;

      }
  }
}
