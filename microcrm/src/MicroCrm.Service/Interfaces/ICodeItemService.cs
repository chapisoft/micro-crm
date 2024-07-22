using System;
using System.Data;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MicroCrm.Domain.Models;
using URF.Core.Abstractions.Services;


namespace MicroCrm.Service
{
    public interface ICodeItemService:IService<CodeItem>
    {


    Task<string> Code2Text(string type, string code);
    Task<string> Text2Code(string type, string text);
    Task UpdateJavascriptAsync(string filename);
    Task ImportDataTableAsync(DataTable datatable);
		Task<Stream> ExportExcelAsync( string filterRules = "",string sort = "Id", string order = "asc");
	}
}
