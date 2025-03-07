﻿using MicroCrm.Domain.Models;
using Microsoft.Extensions.Logging;
using MicroCrm.Service.Common;

// Sample to extend CustomerService, scoped to only CustomerService vs. application wide
namespace MicroCrm.Service
{
  public class CustomerService : ServiceX<Customer>, ICustomerService
  {
 
    public CustomerService(
      ILogger<Customer> _logger,
      IDataTableImportMappingService _mappingservice,
      IExcelService excelService,
      IRepositoryX<Customer> repository) : base(_mappingservice,_logger, excelService, repository)
    {
      
    }
 

  
  }
}
