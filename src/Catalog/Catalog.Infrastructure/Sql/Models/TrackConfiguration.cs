﻿using System;
using System.Collections.Generic;
using System.Text;
using Catalog.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Sql.Models
{
    internal sealed class TrackConfiguration : IEntityTypeConfiguration<Track>
    {

        public void Configure(EntityTypeBuilder<Track> entity)
        {
            entity.ToTable("track", "music_catalog");
            entity.HasKey(e => e.Id).HasName("pk_track");
            entity.Property(e => e.Id).HasColumnName("track_id");
            entity.Property(entity => entity.Composer).HasColumnName("composer");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.UnitPrice).HasColumnName("unit_price");
            entity.Property(e => e.Bytes).HasColumnName("bytes");
            entity.Property(e => e.Milliseconds).HasColumnName("milliseconds");

            // map genre relationship
            entity.Property(e => e.GenreId).HasColumnName("genre_id");
            entity.HasOne(e => e.Genre);

            // map album relationship
            entity.Property(e => e.AlbumId).HasColumnName("album_id");
            entity.HasOne(e => e.Album);

            // map mediatype relationship
            entity.Property(e => e.MediaTypeId).HasColumnName("media_type_id");
            entity.HasOne(e => e.MediaType);

            // map playlist tracks relationship
            entity.HasMany(e => e.PlaylistTracks).WithOne(e => e.Track!);
        }

    }
}
