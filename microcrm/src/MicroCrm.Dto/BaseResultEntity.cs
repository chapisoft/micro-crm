using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroCrm.Dto
{
  public class BaseEntity
  {
    public int ErrorCode { get; set; } = 200;
    public string Message { get; set; }
  }

  public class Pagination
  {
    public int Count { set; get; }
    public int PageIndex { set; get; }
    public int PageSize { set; get; }
    public int TotalPage
    {
      get
      {
        return (int)Math.Ceiling((float)Count / (float)PageSize);
      }
    }

  }

  // Generic 1 hàm vào
  public class BaseObjectEntity<T> : BaseEntity
  {
    public T Data { get; set; }
    public Pagination Pagination { get; set; }

    //public static implicit operator BaseObjectEntity<T>(BaseObjectEntity<List<ProductDto>> v)
    //{
    //  throw new NotImplementedException();
    //}

    //public static explicit operator BaseObjectEntity<T>(SalaryEmployeeReponsiveDetails v)
    //{
    //  throw new NotImplementedException();
    //}
  }
  public class DaTa
  {
    public Guid? data { get; set; }
    public int? statusCode { get; set; } = 200;
    public bool? succeeded { get; set; }
    public string code { get; set; }
    public string message { get; set; }

  }
}
