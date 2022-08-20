using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PointNow.API.Data.Context
{
    public class ConnectionFactory : IDesignTimeDbContextFactory<PointNowContext>
    {
        public PointNowContext CreateDbContext(string[] args)
        {
            var conn = "Server=(localdb)\\mssqllocaldb;Database=PointNowDb;";
            var optionsBuilder = new DbContextOptionsBuilder<PointNowContext>();

            optionsBuilder.UseSqlServer(conn);
            return new PointNowContext(optionsBuilder.Options);
        }
    }
}
