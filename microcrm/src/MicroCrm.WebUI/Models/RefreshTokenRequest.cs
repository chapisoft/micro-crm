using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MicroCrm.WebUI.Models
{
  public class RefreshTokenRequest
  {
    [Required]
    public string AccessToken { get; set; }

    [Required]
    public string RefreshToken { get; set; }
  }
}
