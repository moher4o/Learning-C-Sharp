using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MakeFriends.Data.Models;

namespace MakeFriends.Data
{
    public class FriendsDbContext : IdentityDbContext<User>
    {
        public DbSet<UserPhoto> Images { get; set; }

        public DbSet<Visitor> Visitors { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Favorite> Favorites { get; set; }

        public FriendsDbContext(DbContextOptions<FriendsDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Favorite>()
                .HasKey(f => new { f.UserId, f.FavoriteUserId });

            builder.Entity<Favorite>()
                .HasOne(f => f.User)
                .WithMany(u => u.FavoriteUsers)
                .HasForeignKey(f => f.UserId);

            builder.Entity<Visitor>()
                .HasKey(v => new { v.ObserverId, v.VisitedUserId });

            builder.Entity<User>()
                .HasMany(u => u.SendMessages)
                .WithOne(m => m.Sender)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>()
                .HasMany(u => u.ReceivedMesages)
                .WithOne(m => m.Receiver)
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>()
                .HasMany(u => u.Images)
                .WithOne(i => i.Individual)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<User>()
                .HasMany(u => u.Visits)
                .WithOne(v => v.VisitedUser)
                .HasForeignKey(v => v.VisitedUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>()
                .HasMany(u => u.Observed)
                .WithOne(v => v.Observer)
                .HasForeignKey(v => v.ObserverId)
                .OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(builder);
        }
    }
}
