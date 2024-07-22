using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using URF.Core.EF.Trackable;

namespace MicroCrm.Domain.Models
{
  public partial class MenuItem : Entity
  {
    public MenuItem()
    {
      Children = new HashSet<MenuItem>();
    }

    [Display(Name ="Title",Description = "Title")]
    [MaxLength(38)]
    [Required]
    public virtual string Title { get; set; }
    [Display(Name = "Description", Description = "Description")]
    [MaxLength(128)]
    public virtual string Description { get; set; }
    [Display(Name = "LineNo", Description = "LineNo")]
    [MaxLength(5)]
    [Required]
    public virtual string LineNum { get; set; }
    [Display(Name = "url", Description = "url")]
    [MaxLength(256)]
    [Required]
    public virtual string Url { get; set; }
    [Display(Name = "controller", Description = "controller")]
    [MaxLength(128)]
    public virtual string Controller { get; set; }
    [Display(Name = "action", Description = "action")]
    [MaxLength(128)]
    public virtual string Action { get; set; }
    [Display(Name = "icon", Description = "icon")]
    [StringLength(30)]
    public virtual string Icon { get; set; }
  
    [Display(Name = "target", Description = "target")]
    [MaxLength(128)]
    [DefaultValue("_self")]
    public virtual string Target { get; set; }
    [Display(Name = "Roles", Description = "Roles")]
    [MaxLength(256)]
    public virtual string Roles { get; set; }
    [Display(Name = "IsEnabled", Description = "IsEnabled")]
    [DefaultValue(true)]
    public virtual  bool IsEnabled { get; set; }
    [Display(Name = "Children", Description = "Children")]
    public virtual ICollection<MenuItem> Children { get; set; }
    [Display(Name = "ParentId", Description = "ParentId")]
    public virtual  int? ParentId { get; set; }
    [Display(Name = "ParentId", Description = "ParentId")]
    [ForeignKey("ParentId")]
    public virtual MenuItem Parent { get; set; }
  }
}
