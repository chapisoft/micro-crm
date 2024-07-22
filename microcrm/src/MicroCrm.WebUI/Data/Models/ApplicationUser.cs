﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MicroCrm.WebUI.Data.Models
{
  public class ApplicationUser : IdentityUser
  {
    [Display(Name = "昵称")]
    [MaxLength(128)]
    public string GivenName { get; set; }
    [Display(Name = "租户数据库")]
    [MaxLength(128)]
    public string TenantDb { get; set; }
    [Display(Name = "租户名称")]
    [MaxLength(128)]
    public string TenantName { get; set; }
    [Display(Name = "是否在线")]
    public bool IsOnline { get; set; }
    [Display(Name = "是否开启聊天功能")]
    public bool EnabledChat { get; set; }
    [Display(Name = "头像")]
    [MaxLength(256)]
    public string AvatarUrl { get; set; }
    [Display(Name = "租户")]
    public int TenantId { get; set; }

  }


  [Table("AspNetTenants")]
  public partial class Tenant
  {
    [Key]
    public int Id { get; set; }
    [Display(Name = "租户名称", Description = "租户名称")]
    [MaxLength(128)]
    [Required]
    public string Name { get; set; }
    [Display(Name = "数据库连接", Description = "数据库连接")]
    [MaxLength(128)]
    public string ConnectionStrings { get; set; }
    [Display(Name = "说明", Description = "说明")]
    [MaxLength(256)]
    public string Description { get; set; }
    [Display(Name = "IsDisabled", Description = "IsDisabled")]
    public bool Disabled { get; set; }
    [NotMapped]
    public int TrackingState { get; set; }


  }
}
