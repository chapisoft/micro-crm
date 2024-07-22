using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using URF.Core.EF.Trackable;

namespace MicroCrm.Domain.Models
{
  public partial class CodeItem : Entity
  {
    [MaxLength(50),
      Display(Name = "Code", Description = "Code", Prompt = "Code"),
      Required(ErrorMessage = "Required")]
    public virtual string Code { get; set; }
    [MaxLength(50),
      Display(Name = "Text", Description = "Text", Prompt = "Text"),
      Required(ErrorMessage = "Required")]
    public virtual string Text { get; set; }
    [MaxLength(20),
      Display(Name = "CodeType", Description = "CodeType", Prompt = "CodeType"),
      Required(ErrorMessage = "Required")]
    public virtual string CodeType { get; set; }
    [MaxLength(128), Display(Name = "Description", Description = "Description", Prompt = "Description"), Required(ErrorMessage = "Required")]
    public virtual string Description { get; set; }
    [Display(Name = "IsDisabled", Description = "IsDisabled", Prompt = "IsDisabled")]
    public virtual  int IsDisabled { get; set; }
    [Display(Name = "Multiple", Description = "Multiple", Prompt = "Multiple")]
    public virtual  bool Multiple { get; set; }

  }
}
