using Catalog.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Sql.Models
{
    internal sealed class MediaTypeConfiguration : IEntityTypeConfiguration<MediaType>
    {

        public void Configure(EntityTypeBuilder<MediaType> entity)
        {
            entity.ToTable("media_type", "music_catalog");
            entity.HasKey(e => e.Id).HasName("pk_media_type");
            entity.Property(e => e.Id).HasColumnName("media_type_id");
            entity.Property(e => e.Name).HasColumnName("name");
        }

    }
}
