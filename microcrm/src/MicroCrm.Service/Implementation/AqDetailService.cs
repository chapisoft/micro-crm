using MicroCrm.Domain.Models;
using Microsoft.Extensions.Logging;
using MicroCrm.Service.Common;

// Sample to extend AqDetailService, scoped to only AqDetailService vs. application wide
namespace MicroCrm.Service
{
  public class AqDetailService : ServiceX<AqDetail>, IAqDetailService
  {
 
    public AqDetailService(
      ILogger<AqDetail> _logger,
      IDataTableImportMappingService _mappingservice,
      IExcelService excelService,
      IRepositoryX<AqDetail> repository) : base(_mappingservice,_logger, excelService, repository)
    {
      
    }
 

  
  }
}
