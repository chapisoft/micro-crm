using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using URF.Core.EF.Trackable;

namespace MicroCrm.Domain.Models
{
  public partial class Notification : Entity
  {

    [Display(Name = "Title", Description = "Title")]
    [Required]
    [MaxLength(128)]
    public virtual string Title { get; set; }
    [Display(Name = "Content", Description = "Content")]
    public virtual string Content { get; set; }
    [Display(Name = "Link", Description = "Link")]
    [MaxLength(256)]
    public virtual string Link { get; set; }
    [Display(Name = "Read", Description = "Read")]
    [DefaultValue(false)]
    public virtual  bool Read { get; set; }
    [Display(Name = "From", Description = "From")]
    public virtual string From { get; set; }
    [Display(Name = "To", Description = "From")]
    public virtual string To { get; set; }
    [Display(Name = "Group", Description = "Group")]
    [MaxLength(20)]
    public virtual string Group { get; set; }
    [Display(Name = "PublishDate", Description = "PublishDate")]
    [DefaultValue("now")]
    public virtual  DateTime PublishDate { get; set; }
    [Display(Name = "Publisher", Description = "Publisher")]
    [MaxLength(128)]
    public virtual string Publisher { get; set; }



  }
}
