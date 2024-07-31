using MicroCrm.Domain.Models;
using Microsoft.Extensions.Logging;
using MicroCrm.Service.Common;

// Sample to extend QuotationService, scoped to only QuotationService vs. application wide
namespace MicroCrm.Service
{
  public class QuotationService : ServiceX<Quotation>, IQuotationService
  {
 
    public QuotationService(
      ILogger<Quotation> _logger,
      IDataTableImportMappingService _mappingservice,
      IExcelService excelService,
      IRepositoryX<Quotation> repository) : base(_mappingservice,_logger, excelService, repository)
    {
      
    }
 

  
  }
}
