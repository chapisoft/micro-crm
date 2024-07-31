using MicroCrm.Domain.Models;
using Microsoft.Extensions.Logging;
using MicroCrm.Service.Common;

// Sample to extend ContactService, scoped to only ContactService vs. application wide
namespace MicroCrm.Service
{
  public class ContactService : ServiceX<Contact>, IContactService
  {
 
    public ContactService(
      ILogger<Contact> _logger,
      IDataTableImportMappingService _mappingservice,
      IExcelService excelService,
      IRepositoryX<Contact> repository) : base(_mappingservice,_logger, excelService, repository)
    {
      
    }
 

  
  }
}
