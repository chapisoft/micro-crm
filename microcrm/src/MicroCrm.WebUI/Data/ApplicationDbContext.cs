using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MicroCrm.WebUI.Data.Models;

namespace MicroCrm.WebUI.Data
{
  public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Log> Logs { get; set; }
  }
}
