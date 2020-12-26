using Catalog.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Sql.Models
{
    internal sealed class ArtistConfiguration : IEntityTypeConfiguration<Artist>
    {

        public void Configure(EntityTypeBuilder<Artist> entity)
        {
            entity.ToTable("artist", "music_catalog");
            entity.HasKey(e => e.Id).HasName("pk_artist");
            entity.Property(e => e.Id).HasColumnName("artist_id");
            entity.Property(e => e.Name).HasColumnName("name");
        }

    }
}
