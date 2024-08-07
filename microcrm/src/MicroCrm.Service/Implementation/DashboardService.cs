using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroCrm.Dto;
using MicroCrm.Repository;
using MicroCrm.Service.Common;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using URF.Core.Abstractions.Trackable;
using URF.Core.Services;

namespace MicroCrm.Service.Implementation
{
  public interface IDashboardService
  {
    Task<BaseEntity> Get(BaseRequest request, string username, string role);
  }

  public class DashboardService : IDashboardService
  {
    private readonly IDashboardRepository _DashboardRepository;

    public DashboardService(IDashboardRepository DashboardRepository)
    {
      _DashboardRepository = DashboardRepository;
    }
    public async Task<BaseEntity> Get(BaseRequest request, string username, string role)
    {
      return await _DashboardRepository.Get(request, username, role);
    }
  }
}
