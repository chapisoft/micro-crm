﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroCrm.Dto;

namespace MicroCrm.Service.Common
{
  public  interface IExcelService
  {
    Task<DataTable> ReadDataTable(Stream inputSteam, string type = ".xlsx");
    Task<Stream> Export<T>( IEnumerable<T> data, ExpColumnOpts[] colopts = null,string name="Sheet1");
    Task<Stream> Export<T>(IEnumerable<T> data, Dictionary<string, Func<T, object>> mappers, string name = "Sheet1");
  }
}
