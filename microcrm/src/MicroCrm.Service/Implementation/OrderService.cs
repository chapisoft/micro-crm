using System.Threading;
using MicroCrm.Domain.Models;
using URF.Core.Abstractions.Trackable;
using URF.Core.Services;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using Microsoft.Extensions.Logging;
using URF.Core.EF;
using MicroCrm.Dto;

// Sample to extend ProductService, scoped to only ProductService vs. application wide
namespace MicroCrm.Service
{
  public class OrderService : Service<Order>, IOrderService
  {
    public OrderService(
      ITrackableRepository<Order> repository) : base(repository)
    {
    }
  }
}
