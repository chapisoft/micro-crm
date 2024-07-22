using System;
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
    [Display(Name = "Nick name")]
    [MaxLength(128)]
    public string GivenName { get; set; }
    [Display(Name = "TenantDb")]
    [MaxLength(128)]
    public string TenantDb { get; set; }
    [Display(Name = "TenantName")]
    [MaxLength(128)]
    public string TenantName { get; set; }
    [Display(Name = "IsOnline")]
    public bool IsOnline { get; set; }
    [Display(Name = "EnabledChat")]
    public bool EnabledChat { get; set; }
    [Display(Name = "AvatarUrl")]
    [MaxLength(256)]
    public string AvatarUrl { get; set; }
    [Display(Name = "Tenant")]
    public int TenantId { get; set; }

  }


  [Table("AspNetTenants")]
  public partial class Tenant
  {
    [Key]
    public int Id { get; set; }
    [Display(Name = "TenantName", Description = "TenantName")]
    [MaxLength(128)]
    [Required]
    public string Name { get; set; }
    [Display(Name = "ConnectionStrings", Description = "ConnectionStrings")]
    [MaxLength(128)]
    public string ConnectionStrings { get; set; }
    [Display(Name = "Description", Description = "Description")]
    [MaxLength(256)]
    public string Description { get; set; }
    [Display(Name = "IsDisabled", Description = "IsDisabled")]
    public bool Disabled { get; set; }
    [NotMapped]
    public int TrackingState { get; set; }


  }
}
