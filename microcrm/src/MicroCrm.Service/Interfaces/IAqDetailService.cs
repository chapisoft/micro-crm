using System;
using System.Data;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MicroCrm.Domain.Models;
using URF.Core.Abstractions.Services;

namespace MicroCrm.Service
{
  // Example: extending IService<TEntity> and/or ITrackableRepository<TEntity>, scope: IAqDetailService
  public interface IAqDetailService : IServiceX<AqDetail>
  {
    
  }
}
