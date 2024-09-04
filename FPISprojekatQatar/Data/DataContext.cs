using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace MobilnoQatarBack.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Grupa> Grupe { get; set; }
        public DbSet<Tim> Timovi { get; set; }
        public DbSet<Stadion> Stadioni { get; set; }
        public DbSet<Utakmica> Utakmice { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Grupa>().HasKey(g => g.Id);
            modelBuilder.Entity<Tim>().HasKey(t => t.Id);
            modelBuilder.Entity<Stadion>().HasKey(s => s.Id);
            modelBuilder.Entity<Utakmica>().HasKey(u => u.Id);
            modelBuilder.Entity<User>().HasKey(u => u.Id);

            modelBuilder.Entity<Grupa>()
                .HasMany(g => g.Timovi)
                .WithOne(t => t.Grupa)
                .HasForeignKey(t => t.GrupaId)
                .IsRequired(false);  // Make group association optional

            modelBuilder.Entity<Utakmica>()
                .HasOne(u => u.Tim1)
                .WithMany()
                .HasForeignKey(u => u.Tim1Id)
                .OnDelete(DeleteBehavior.Restrict);  // Prevent team deletion if involved in matches

            modelBuilder.Entity<Utakmica>()
                .HasOne(u => u.Tim2)
                .WithMany()
                .HasForeignKey(u => u.Tim2Id)
                .OnDelete(DeleteBehavior.Restrict);
            //aaaa
            modelBuilder.Entity<Utakmica>(entity =>
            {
                entity.Property(e => e.Tim1Golovi).IsRequired(false);
                entity.Property(e => e.Tim2Golovi).IsRequired(false);
                entity.Property(e => e.StadionId).IsRequired(false);
            });

            modelBuilder.Entity<Stadion>()
                .HasMany(s => s.Utakmice)
                .WithOne(u => u.Stadion)
                .HasForeignKey(u => u.StadionId)
                .IsRequired(false);  // Make stadium association optional


            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(u => u.FirstName).IsRequired();
                entity.Property(u => u.LastName).IsRequired();
            });
        }
    }
}
