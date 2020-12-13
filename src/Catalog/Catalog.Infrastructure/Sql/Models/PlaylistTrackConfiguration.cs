﻿using Catalog.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Sql.Models
{
    internal sealed class PlaylistTrackConfiguration : IEntityTypeConfiguration<PlaylistTrack>
    {

        public void Configure(EntityTypeBuilder<PlaylistTrack> entity)
        {
            entity.ToTable("playlist_track", "music_catalog");
            entity.HasKey(e => new { e.PlaylistId, e.TrackId }).HasName("pk_playlist_track");

            // map track relationship
            entity.Property(e => e.TrackId).HasColumnName("track_id");
            entity.HasOne(e => e.Track).WithMany(e => e!.PlaylistTracks);

            // map track relationship
            entity.Property(e => e.PlaylistId).HasColumnName("playlist_id");
            entity.HasOne(e => e.Playlist).WithMany(e => e!.PlaylistTracks);
        }

    }
}
