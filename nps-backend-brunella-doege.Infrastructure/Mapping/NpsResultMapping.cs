using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using nps_backend_brunella_doege.Domain.Entidades;
using System.Xml.Linq;

namespace nps_backend_brunella_doege.Infrastructure.Mapping
{
    public class NpsResultMapping : IEntityTypeConfiguration<NpsResult>
    {
        public void Configure(EntityTypeBuilder<NpsResult> builder)
        {
            builder.ToTable("NpsResult");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.SystemId)
                .IsRequired();

            builder.Property(x => x.CreatedDate)
                .IsRequired();

            builder.Property(x => x.CategoryId);

            builder.Property(x => x.Comments);

            builder.Property(x => x.Score)
                .IsRequired();

            builder.Property(x => x.UserId)
                .IsRequired();
        }
    }
}