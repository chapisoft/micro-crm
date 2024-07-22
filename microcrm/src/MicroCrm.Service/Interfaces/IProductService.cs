using System;
using System.Data;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MicroCrm.Domain.Models;
using URF.Core.Abstractions.Services;

namespace MicroCrm.Service
{
  // Example: extending IService<TEntity> and/or ITrackableRepository<TEntity>, scope: ICustomerService
  public interface IProductService : IServiceX<Product>
  {
    

    //Task ImportDataTableAsync(DataTable datatable);
    //Task<Stream> ExportExcelAsync(string filterRules = "", string sort = "Id", string order = "asc");
  }
}
