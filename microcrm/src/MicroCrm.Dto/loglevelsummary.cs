﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MicroCrm.Dto
{
 public class loglevelsummary
  {
    public string level { get; set; }
    public string total { get; set; }

  }
  public class logtimesummary {
    public DateTime time { get; set; }
    public int total { get; set; }
    public string level { get; set; }
  }
}
