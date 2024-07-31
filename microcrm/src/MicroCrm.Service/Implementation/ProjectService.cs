using MicroCrm.Domain.Models;
using Microsoft.Extensions.Logging;
using MicroCrm.Service.Common;

// Sample to extend ApplicationService, scoped to only ApplicationService vs. application wide
namespace MicroCrm.Service
{
  public class ProjectService : ServiceX<Project>, IProjectService
  {
 
    public ProjectService(
      ILogger<Project> _logger,
      IDataTableImportMappingService _mappingservice,
      IExcelService excelService,
      IRepositoryX<Project> repository) : base(_mappingservice,_logger, excelService, repository)
    {
      
    }
 

  
  }
}
