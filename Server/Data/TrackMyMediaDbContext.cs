using Microsoft.EntityFrameworkCore;
using TrackMyMedia.Shared.Models;

namespace TrackMyMedia.Server.Data
{
    public class TrackMyMediaDbContext : DbContext
    {
        public TrackMyMediaDbContext(DbContextOptions<TrackMyMediaDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<MediaItemModel> MediaItems { get; set; }
        public DbSet<MovieModel> Movies { get; set; }
        public DbSet<TVShowModel> TVShows { get; set; }
        public DbSet<BookModel> Books { get; set; }
        public DbSet<VideoGameModel> VideoGames { get; set; }
        public DbSet<MusicModel> Music { get; set; }
        public DbSet<ComicModel> Comics { get; set; }
        public DbSet<AnimeModel> Anime { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserModel>()
                .ToTable("Users")
                .HasKey(u => u.UserId);

            modelBuilder.Entity<MediaItemModel>()
                .ToTable("MediaItems")
                .HasKey(m => m.MediaId);

            // Movie-specific configuration
            modelBuilder.Entity<MovieModel>()
                .ToTable("Movies");

            // TVShow-specific configuration
            modelBuilder.Entity<TVShowModel>()
                .ToTable("TVShows");

            // Book-specific configuration
            modelBuilder.Entity<BookModel>()
                .ToTable("Books");

            // VideoGame-specific configuration
            modelBuilder.Entity<VideoGameModel>()
                .ToTable("VideoGames");

            // Music-specific configuration
            modelBuilder.Entity<MusicModel>()
                .ToTable("Music");

            // Comic-specific configuration
            modelBuilder.Entity<ComicModel>()
                .ToTable("Comics");

            // Anime-specific configuration
            modelBuilder.Entity<AnimeModel>()
                .ToTable("Anime");
        }
    }
}
