using Catalog.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Sql.Models
{
    internal sealed class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {

        public void Configure(EntityTypeBuilder<Genre> entity)
        {
            entity.ToTable("genre", "music_catalog");
            entity.HasKey(e => e.Id).HasName("pk_genre");
            entity.Property(e => e.Id).HasColumnName("genre_id");
            entity.Property(e => e.Name).HasColumnName("name");
        }

    }
}
