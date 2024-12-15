using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystemApp.database
{
    internal class ApplicationDbContext : DbContext
    {
        public DbSet<Profession> Professions { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        {
            // если честно я уже подзабыл как это правильно настраивается
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Используем SQLite
            string databaseFolder = DbRepository.GetDatabaseFolder();
            string dbName = "professions.db";
            string filpath = Path.Combine(databaseFolder, dbName);
            optionsBuilder.UseSqlite($"Data Source={filpath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Определяем сущность Profession
            modelBuilder.Entity<Profession>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(1000);
            });
        }
    }
}
