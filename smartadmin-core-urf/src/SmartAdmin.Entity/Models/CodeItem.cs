﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using URF.Core.EF.Trackable;

namespace SmartAdmin.Domain.Models
{
  public partial class CodeItem : Entity
  {
    [MaxLength(50),
      Display(Name = "值", Description = "值", Prompt = "值"),
      Required(ErrorMessage = "必填")]
    public virtual string Code { get; set; }
    [MaxLength(50),
      Display(Name = "显示", Description = "显示", Prompt = "显示"),
      Required(ErrorMessage = "必填")]
    public virtual string Text { get; set; }
    [MaxLength(20),
      Display(Name = "代码名称", Description = "代码名称", Prompt = "代码名称"),
      Required(ErrorMessage = "必填")]
    public virtual string CodeType { get; set; }
    [MaxLength(128), Display(Name = "描述", Description = "描述", Prompt = "描述"), Required(ErrorMessage = "必填")]
    public virtual string Description { get; set; }
    [Display(Name = "是否禁用", Description = "是否禁用", Prompt = "是否禁用")]
    public virtual  int IsDisabled { get; set; }
    [Display(Name = "是否支持多选", Description = "是否支持多选", Prompt = "是否支持多选")]
    public virtual  bool Multiple { get; set; }

  }
}
