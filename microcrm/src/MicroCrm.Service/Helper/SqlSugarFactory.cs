﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;

namespace MicroCrm.Service.Helper
{
  public static class SqlSugarFactory
  {
    public static ISqlSugarClient CreateSqlSugarClient(IServiceProvider serviceProvider)
    {
      var configuration = serviceProvider.GetRequiredService<IConfiguration>();
      var connectionString = configuration.GetConnectionString("MicroCrmDbContext");
      var db = new SqlSugarClient(new ConnectionConfig()
      {
        ConnectionString = connectionString,
        DbType = DbType.SqlServer, //Required（那个数据库）
        IsAutoCloseConnection = true, //默认false（是否自动关闭连接）
        InitKeyType = InitKeyType.Attribute
      });  //默认SystemTable
      return db;
    }
  }
}
