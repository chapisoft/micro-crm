using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URF.Core.EF.Trackable;

namespace MicroCrm.Domain.Models
{
  public partial class Photo:Entity
  {
    [Required]
    [Display(Name = "Name", Description = "Name", Prompt = "Name")]
    [MaxLength(128)]
    public string Name { get; set; }
    [Required]
    [Display(Name = "Path", Description = "Path", Prompt = "Path")]
    [MaxLength(512)]
    public string Path { get; set;}
    [Display(Name = "Size", Description = "Size", Prompt = "Size")]
    public decimal Size { get; set; }
    [Display(Name = "Landmarks", Description = "Landmarks", Prompt = "Landmarks")]
    public string Landmarks { get; set; }
  }
}
