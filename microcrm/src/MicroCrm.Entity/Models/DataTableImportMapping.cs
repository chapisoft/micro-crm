using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using URF.Core.EF.Trackable;

namespace MicroCrm.Domain.Models
{
  public partial class DataTableImportMapping : Entity
  {

    [Required]
    [MaxLength(50)]
    [Display(Name = "EntitySetName", Description = "EntitySetName")]
    public virtual string EntitySetName { get; set; }
    [Required]
    [MaxLength(50)]
    [Display(Name = "FieldName", Description = "FieldName")]
    public virtual string FieldName { get; set; }
    [MaxLength(50)]
    [Display(Name = "DefaultValue", Description = "DefaultValue")]
    public virtual string DefaultValue { get; set; }
    [Display(Name = "TypeName", Description = "TypeName")]
    [MaxLength(30)]
    public virtual string TypeName { get; set; }
    [Display(Name = "IsRequired", Description = "IsRequired")]
    [DefaultValue(false)]
    public virtual  bool IsRequired { get; set; }
    [MaxLength(50)]
    [Display(Name = "SourceFieldName", Description = "SourceFieldName")]
    public virtual string SourceFieldName { get; set; }
    [Display(Name = "IsEnabled", Description = "IsEnabled")]
    [DefaultValue(true)]
    public virtual  bool IsEnabled { get; set; }
    [Display(Name = "Exportable", Description = "Exportable")]
    [DefaultValue(false)]
    public virtual  bool Exportable { get; set; }

    [Display(Name = "RegularExpression", Description = "RegularExpression")]
    [MaxLength(100)]
    public virtual string RegularExpression { get; set; }
    [Display(Name = "LineNo", Description = "LineNo")]
    public virtual int LineNo { get; set; }

  }
  
  }
   
