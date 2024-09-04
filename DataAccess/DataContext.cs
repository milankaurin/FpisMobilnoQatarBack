using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Primarni ključevi
            modelBuilder.Entity<Grupa>().HasKey(g => g.Id);
            modelBuilder.Entity<Tim>().HasKey(t => t.Id);
            modelBuilder.Entity<Stadion>().HasKey(s => s.Id);
            modelBuilder.Entity<Utakmica>().HasKey(u => u.Id);

            // Veze između entiteta
            // Grupa-Tim (jedan prema mnogo)
            modelBuilder.Entity<Grupa>()
                .HasMany(g => g.Timovi)
                .WithOne(t => t.Grupa)
                .HasForeignKey(t => t.GrupaId);

            // Tim-Utakmica (mnogo prema mnogo, koristi se pomoćna tabela za predstavljanje)
            modelBuilder.Entity<Utakmica>()
                .HasOne(u => u.Tim1)
                .WithMany(t => t.DomaceUtakmice)
                .HasForeignKey(u => u.Tim1Id)
                .OnDelete(DeleteBehavior.Restrict); // Sprečavanje brisanja tima ako su uključeni u utakmice

            modelBuilder.Entity<Utakmica>()
                .HasOne(u => u.Tim2)
                .WithMany(t => t.GostujuceUtakmice)
                .HasForeignKey(u => u.Tim2Id)
                .OnDelete(DeleteBehavior.Restrict);

            // Stadion-Utakmica (jedan prema mnogo)
            modelBuilder.Entity<Stadion>()
                .HasMany(s => s.Utakmice)
                .WithOne(u => u.Stadion)
                .HasForeignKey(u => u.StadionId);
        }
    }
}