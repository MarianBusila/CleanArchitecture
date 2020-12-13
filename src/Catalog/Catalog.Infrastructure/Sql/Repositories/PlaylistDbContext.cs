using System;
using Catalog.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Sql.Repositories
{
    public class PlaylistDbContext : DbContext
    {

        public DbSet<Playlist> Playlists { get; set; } = null;
        public DbSet<PlaylistTrack> PlaylistTracks { get; set; } = null;

        public PlaylistDbContext(DbContextOptions<PlaylistDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if(modelBuilder is null)
                throw  new ArgumentNullException(nameof(modelBuilder));

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PlaylistDbContext).Assembly);
        }

    }
}
