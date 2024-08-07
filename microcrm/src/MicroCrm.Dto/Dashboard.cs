using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroCrm.Dto
{
  public partial class Dashboard
  {
    public StatisticYear StatisticYear { get; set; }
    public StatisticStatus StatisticStatus { get; set; }
    public StatisticRank StatisticRank { get; set; }
  }
  public partial class StatisticYear
  {
    public int Year { get; set; }
    public int Month { get; set; }
    public decimal Amount { get; set; }
    public string Man { get; set; }
    public decimal TopOfSale { get; set; }
  }
  public partial class StatisticStatus
  {
    public string Status { get; set; }
    public decimal Qty { get; set; }
    public decimal Per { get; set; }
  }
  public partial class StatisticRank
  {
    public string Man { get; set; }
    public decimal Qty { get; set; }
    public decimal Per { get; set; }
  }
}
