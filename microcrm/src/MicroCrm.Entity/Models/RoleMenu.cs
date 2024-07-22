using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using URF.Core.EF.Trackable;

namespace MicroCrm.Domain.Models
{
  public partial class RoleMenu : Entity
  {
  
    [Display(Name ="RoleName",Description = "RoleName")]
    [MaxLength(128)]
    [Required]
    public virtual string RoleName { get; set; }

    [Display(Name = "Menu", Description = "Menu")]
    public virtual  int MenuId { get; set; }
    [Display(Name = "Menu", Description = "Menu")]
    [ForeignKey("MenuId")]
    public virtual MenuItem MenuItem { get; set; }
    [Display(Name = "IsEnabled", Description = "IsEnabled")]
    [DefaultValue(true)]
    public virtual  bool IsEnabled { get; set; }
    [Display(Name = "Create", Description = "Create")]
    [DefaultValue(true)]
    public virtual  bool Create { get; set; }
    [Display(Name = "Edit", Description = "Edit")]
    [DefaultValue(true)]
    public virtual  bool Edit { get; set; }
    [Display(Name = "Delete", Description = "Delete")]
    [DefaultValue(true)]
    public virtual  bool Delete { get; set; }
    [Display(Name = "Import", Description = "Import")]
    [DefaultValue(true)]
    public virtual  bool Import { get; set; }
    [Display(Name = "Export", Description = "Export")]
    [DefaultValue(true)]
    public virtual  bool Export { get; set; }
    [Display(Name = "FunctionPoint1", Description = "FunctionPoint1")]
    [DefaultValue(true)]
    public virtual  bool FunctionPoint1 { get; set; }
    [Display(Name = "FunctionPoint2", Description = "FunctionPoint2")]
    [DefaultValue(true)]
    public virtual  bool FunctionPoint2 { get; set; }
    [Display(Name = "FunctionPoint3", Description = "FunctionPoint3")]
    [DefaultValue(true)]
    public virtual  bool FunctionPoint3 { get; set; }
  }
}

