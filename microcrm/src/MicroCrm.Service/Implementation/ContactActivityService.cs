using MicroCrm.Domain.Models;
using Microsoft.Extensions.Logging;
using MicroCrm.Service.Common;

// Sample to extend ActivityService, scoped to only ActivityService vs. application wide
namespace MicroCrm.Service
{
  public class ContactActivityService : ServiceX<ContactActivity>, IContactActivityService
  {
 
    public ContactActivityService(
      ILogger<ContactActivity> _logger,
      IDataTableImportMappingService _mappingservice,
      IExcelService excelService,
      IRepositoryX<ContactActivity> repository) : base(_mappingservice,_logger, excelService, repository)
    {
      
    }
 

  
  }
}
