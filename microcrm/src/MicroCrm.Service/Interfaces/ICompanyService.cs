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
  public interface ICompanyService : IService<Company>
  {
    // Example: adding synchronous Single method, scope: ICustomerService
    Company Single(Expression<Func<Company, bool>> predicate);

    Task ImportDataTableAsync(Stream stream);
    Task<Stream> ExportExcelAsync(string filterRules = "", string sort = "Id", string order = "asc");
  }
}
