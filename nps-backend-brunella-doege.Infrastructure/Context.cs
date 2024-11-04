using Microsoft.EntityFrameworkCore;
using nps_backend_brunella_doege.Domain.Entities;
using nps_backend_brunella_doege.Infrastructure.Mapping;

namespace nps_backend_brunella_doege.Infrastructure
{
    public class Context : DbContext
    {
        public DbSet<NpsResult> NpsResult { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new NpsResultMapping());
            base.OnModelCreating(modelBuilder);
        }
    }
}
