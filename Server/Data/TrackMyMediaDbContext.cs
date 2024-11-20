using Microsoft.EntityFrameworkCore;
using TrackMyMedia.Server.Data;

namespace TrackMyMedia.Server.Data
{
    public class TrackMyMediaDbContext : DbContext
    {
        public TrackMyMediaDbContext(DbContextOptions<TrackMyMediaDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserModel>()
                .ToTable("Users")  // Map 'UserModel' class to 'Users' table in the database
                .HasKey(u => u.UserId);  // Set UserId as the primary key
        }
    }
}
