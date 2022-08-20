
using Microsoft.EntityFrameworkCore;
using PointNow.API.Data.Interfaces;
using PointNow.API.Domain.Services;
using System.Data.Common;

namespace PointNow.API.Data.Context
{
    public class PointNowContext : DbContext, IDbContext
    {
        #region Ctor
#pragma warning disable CS8618
        public PointNowContext(DbContextOptions<PointNowContext> options) : base(options)
        {
        }
        #endregion

        #region Methods
        public DbConnection GetConnection()
        {
            return Database.GetDbConnection();
        }
        #endregion

        #region DbSets
        public DbSet<User> Users { get; set; }
        #endregion

        #region Override
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("AspNetUsers", "Identity");
        }
        #endregion
    }
}
