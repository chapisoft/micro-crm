﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MicroCrm.WebUI.ViewComponents
{
  public class ImportExcelOptions{
    public string url { get; set; }
    public string tpl { get; set; }
    public string folder { get; set; }
    public string entity { get; set; }
    public string callback { get; set; }
    public bool autosave { get; set; } = false;
    public string[] properties { get; set; }
  }
  public class ImportExcelViewComponent : ViewComponent
  {
    public async Task<IViewComponentResult> InvokeAsync(ImportExcelOptions options)
    {
      return await Task.FromResult((IViewComponentResult)View(options));
    }
  }
}
