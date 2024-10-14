using Microsoft.EntityFrameworkCore;
using nps_backend_brunella_doege.Domain.Entidades;
using nps_backend_brunella_doege.Infrastructure.Mapping;

namespace nps_backend_brunella_doege.Infrastructure
{
    public class Contexto : DbContext
    {
        public DbSet<NpsResult> NpsResult { get; set; }

        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new NpsResultMapping());
            base.OnModelCreating(modelBuilder);
        }
    }
}
