using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using URF.Core.EF.Trackable;

namespace MicroCrm.Domain.Models
{
  public partial class Product:Entity
  {
    [Required]
    [Display(Name = "Name", Description = "Name", Prompt  = "Name")]
    [MaxLength(128)]
    public virtual string Name { get; set; }
    [Display(Name = "Model", Description = "Model", Prompt = "Model")]
    [MaxLength(60)]
    public virtual string Model { get; set; }
    [Display(Name = "Unit", Description = "Unit", Prompt = "Unit")]
    [MaxLength(10)]
    public virtual string Unit { get; set; }
    [Display(Name = "Price", Description = "Price", Prompt = "Price")]
    [Column(TypeName = "decimal(18, 2)")]
    public virtual  decimal UnitPrice { get; set; }
    [Display(Name = "ImagePath", Description = "ImagePath", Prompt = "ImagePath")]
    public virtual string ImagePath { get; set; }
    [Display(Name = "Description", Description = "Description", Prompt = "Description")]
    public virtual string Description { get; set; }
    [Display(Name = "Private", Description = "Private", Prompt = "Private")]
    public virtual int Private { get; set; }
    [Display(Name = "Name En", Description = "Name En", Prompt = "Name En")]
    public virtual string NameEn { get; set; }
    [Display(Name = "Description En", Description = "Description En", Prompt = "Description En")]
    public virtual string DescriptionEn { get; set; }
    [Display(Name = "Brand", Description = "Brand", Prompt = "Brand")]
    public virtual string Brand { get; set; }
    
  }
}
