using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Catalog.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "music_catalog");

            migrationBuilder.CreateTable(
                name: "artist",
                schema: "music_catalog",
                columns: table => new
                {
                    artist_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_artist", x => x.artist_id);
                });

            migrationBuilder.CreateTable(
                name: "genre",
                schema: "music_catalog",
                columns: table => new
                {
                    genre_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_genre", x => x.genre_id);
                });

            migrationBuilder.CreateTable(
                name: "media_type",
                schema: "music_catalog",
                columns: table => new
                {
                    media_type_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_media_type", x => x.media_type_id);
                });

            migrationBuilder.CreateTable(
                name: "playlist",
                schema: "music_catalog",
                columns: table => new
                {
                    playlist_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_playlist", x => x.playlist_id);
                });

            migrationBuilder.CreateTable(
                name: "album",
                schema: "music_catalog",
                columns: table => new
                {
                    album_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: true),
                    artist_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_album", x => x.album_id);
                    table.ForeignKey(
                        name: "FK_album_artist_artist_id",
                        column: x => x.artist_id,
                        principalSchema: "music_catalog",
                        principalTable: "artist",
                        principalColumn: "artist_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "track",
                schema: "music_catalog",
                columns: table => new
                {
                    track_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    composer = table.Column<string>(type: "text", nullable: true),
                    name = table.Column<string>(type: "text", nullable: true),
                    unit_price = table.Column<decimal>(type: "numeric", nullable: false),
                    bytes = table.Column<int>(type: "integer", nullable: false),
                    milliseconds = table.Column<int>(type: "integer", nullable: false),
                    genre_id = table.Column<int>(type: "integer", nullable: false),
                    album_id = table.Column<int>(type: "integer", nullable: false),
                    media_type_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_track", x => x.track_id);
                    table.ForeignKey(
                        name: "FK_track_album_album_id",
                        column: x => x.album_id,
                        principalSchema: "music_catalog",
                        principalTable: "album",
                        principalColumn: "album_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_track_genre_genre_id",
                        column: x => x.genre_id,
                        principalSchema: "music_catalog",
                        principalTable: "genre",
                        principalColumn: "genre_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_track_media_type_media_type_id",
                        column: x => x.media_type_id,
                        principalSchema: "music_catalog",
                        principalTable: "media_type",
                        principalColumn: "media_type_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "playlist_track",
                schema: "music_catalog",
                columns: table => new
                {
                    playlist_id = table.Column<int>(type: "integer", nullable: false),
                    track_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_playlist_track", x => new { x.playlist_id, x.track_id });
                    table.ForeignKey(
                        name: "FK_playlist_track_playlist_playlist_id",
                        column: x => x.playlist_id,
                        principalSchema: "music_catalog",
                        principalTable: "playlist",
                        principalColumn: "playlist_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_playlist_track_track_track_id",
                        column: x => x.track_id,
                        principalSchema: "music_catalog",
                        principalTable: "track",
                        principalColumn: "track_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_album_artist_id",
                schema: "music_catalog",
                table: "album",
                column: "artist_id");

            migrationBuilder.CreateIndex(
                name: "IX_playlist_track_track_id",
                schema: "music_catalog",
                table: "playlist_track",
                column: "track_id");

            migrationBuilder.CreateIndex(
                name: "IX_track_album_id",
                schema: "music_catalog",
                table: "track",
                column: "album_id");

            migrationBuilder.CreateIndex(
                name: "IX_track_genre_id",
                schema: "music_catalog",
                table: "track",
                column: "genre_id");

            migrationBuilder.CreateIndex(
                name: "IX_track_media_type_id",
                schema: "music_catalog",
                table: "track",
                column: "media_type_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "playlist_track",
                schema: "music_catalog");

            migrationBuilder.DropTable(
                name: "playlist",
                schema: "music_catalog");

            migrationBuilder.DropTable(
                name: "track",
                schema: "music_catalog");

            migrationBuilder.DropTable(
                name: "album",
                schema: "music_catalog");

            migrationBuilder.DropTable(
                name: "genre",
                schema: "music_catalog");

            migrationBuilder.DropTable(
                name: "media_type",
                schema: "music_catalog");

            migrationBuilder.DropTable(
                name: "artist",
                schema: "music_catalog");
        }
    }
}
