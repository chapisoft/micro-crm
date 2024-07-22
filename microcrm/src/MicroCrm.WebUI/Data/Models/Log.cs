using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MicroCrm.WebUI.Data.Models
{
  [Table("AspNetLogs")]
  public partial class Log 
    {
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int Id { get; set; }
      [Display(Name = "MachineName", Description = "MachineName")]
      [MaxLength(128)]
      public string MachineName { get; set; }
      [Display(Name = "Logged", Description = "Logged")]
      public DateTime? Logged { get; set; }
      [Display(Name = "Level", Description = "Level")]
      [MaxLength(15)]
      public string Level { get; set; }
      [Display(Name = "Message", Description = "Message")]
      public string Message { get; set; }
      [Display(Name = "ErrorMessage", Description = "ErrorMessage")]
      public string Exception { get; set; }
      [Display(Name = "RequestIp", Description = "RequestIp")]
      [MaxLength(32)]
      public string RequestIp { get; set; }
      [Display(Name = "Properties", Description = "Properties")]
      public string Properties { get; set; }
      [Display(Name = "RequestForm", Description = "RequestForm")]
      public string RequestForm { get; set; }
      [Display(Name = "Identity", Description = "Identity")]
      [MaxLength(128)]
      public string Identity { get; set; }
      [Display(Name = "Logger", Description = "Logger")]
      [MaxLength(256)]
      public string Logger { get; set; }
      [Display(Name = "Callsite", Description = "Callsite")]
      [MaxLength(256)]
      public string Callsite { get; set; }
      [Display(Name = "SiteName", Description = "SiteName")]
      [MaxLength(128)]
      public string SiteName { get; set; }
      [Display(Name = "Resolved", Description = "Resolved")]
      [DefaultValue(false)]
      public bool Resolved { get; set; }
    [Display(Name = "User-Agent", Description = "User-Agent")]
    [MaxLength(128)]
    public string UserAgent { get; set; }
  }
  
}
