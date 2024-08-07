using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MicroCrm.Domain.Models;

namespace MicroCrm.Application.Projects.Commands
{
  public class CreateOrEditProjectCommand:IRequest<Project>
  {
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public int ContactId { get; set; }
    public string Name { get; set; }
    public string Title { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Competitors { get; set; }
    public decimal TotalVolume { get; set; }
    public string Note { get; set; }
    public int Status { get; set; }
    public string ApprovedBy { get; set; }
    public DateTime ApprovedDate { get; set; }
    public string Asignee { get; set; }
    
  }
}
