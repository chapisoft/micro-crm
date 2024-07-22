﻿using System;
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
      [Display(Name = "主机名", Description = "主机名")]
      [MaxLength(128)]
      public string MachineName { get; set; }
      [Display(Name = "时间", Description = "时间")]
      public DateTime? Logged { get; set; }
      [Display(Name = "级别", Description = "级别")]
      [MaxLength(15)]
      public string Level { get; set; }
      [Display(Name = "信息", Description = "信息")]
      public string Message { get; set; }
      [Display(Name = "异常信息", Description = "异常信息")]
      public string Exception { get; set; }
      [Display(Name = "请求IP", Description = "请求IP")]
      [MaxLength(32)]
      public string RequestIp { get; set; }
      [Display(Name = "事件属性", Description = "事件属性")]
      public string Properties { get; set; }
      [Display(Name = "表单值", Description = "表单值")]
      public string RequestForm { get; set; }
      [Display(Name = "账号", Description = "账号")]
      [MaxLength(128)]
      public string Identity { get; set; }
      [Display(Name = "日志记录器", Description = "日志记录器")]
      [MaxLength(256)]
      public string Logger { get; set; }
      [Display(Name = "日志来源", Description = "日志来源")]
      [MaxLength(256)]
      public string Callsite { get; set; }
      [Display(Name = "网站名", Description = "网站名")]
      [MaxLength(128)]
      public string SiteName { get; set; }
      [Display(Name = "已处理", Description = "已处理")]
      [DefaultValue(false)]
      public bool Resolved { get; set; }
    [Display(Name = "User-Agent", Description = "User-Agent")]
    [MaxLength(128)]
    public string UserAgent { get; set; }
  }
  
}
