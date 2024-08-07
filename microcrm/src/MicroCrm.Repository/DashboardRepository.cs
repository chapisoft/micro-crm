using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MicroCrm.Dto;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TrackableEntities.Common.Core;
using URF.Core.Abstractions.Trackable;
using URF.Core.EF;
using URF.Core.EF.Trackable;

namespace MicroCrm.Repository
{
  public interface IDashboardRepository
  {
    Task<BaseEntity> Get(BaseRequest request, string username, string role);
  }
  public class DashboardRepository : IDashboardRepository
  {
    private readonly DbContext _context;
    private readonly IConfiguration _configuration;
    private readonly HttpClient _client = new HttpClient();
    public DashboardRepository(IConfiguration configuration, DbContext context)
    {
      _configuration = configuration;
      _context = context;
    }
    public string GetConnection()
    {
      var connection = _configuration.GetSection("ConnectionStrings").GetSection("MicroCrmDbContext").Value;
      return connection;
    }

    public async Task<BaseEntity> Get(BaseRequest request, string username, string role)
    {
      var connectionString = this.GetConnection();
      using var con = new SqlConnection(connectionString);
      try
      {
        con.Open();

        var query = "Proc_Dashboard_Get";

        var result = await con.QueryAsync<Dashboard>(query,
            new
            {
              User = username,
              Role = role,
              FromDate = request.FromDate,
              ToDate = request.ToDate
            }, commandType: System.Data.CommandType.StoredProcedure);

        var listModel = result.AsList();

        var newResult = new BaseObjectEntity<List<Dashboard>>
        {
          Data = result.AsList(),
          Pagination = new Pagination()
          {
            Count = listModel.Count,
            PageSize = (int)request.PageSize,
            PageIndex = (int)request.PageIndex
          }
        };

        return newResult;
      }
      catch (Exception ex)
      {
        //Log.Error(ex.Message);
        var exception = new BaseEntity
        {
          ErrorCode = 400,
          Message = ex.Message
        };
        return exception;
      }
      finally
      {
        con.Close();
      }
    }
  }
}
