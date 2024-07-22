using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MicroCrm.Infrastructure;

namespace MicroCrm.Infrastructure.Persistence
{
    public class MicroCrmDbContextFactory : IDesignTimeDbContextFactory<MicroCrmDbContext>
    {
        public MicroCrmDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MicroCrmDbContext>();
            optionsBuilder.UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=SmartDb;Trusted_Connection=True;");
            return new MicroCrmDbContext(optionsBuilder.Options,null);
        }
    }
}
