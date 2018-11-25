using AccessFacade.Configuration;
using AccessFacade.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccessFacade.Dal.Context
{
    public class TestDbContext : DbContext
    {
        private readonly string connectionString = "Server=(localdb)\\mssqllocaldb;Database=BakalarskaPrace;Trusted_Connection=True;MultipleActiveResultSets=true";

        public TestDbContext()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<UserTest> UserTest { get; set; }
        public DbSet<UserTestInsert> UserTestInsert { get; set; }
        public DbSet<UserTestUpdate> UserTestUpdate { get; set; }
        public DbSet<UserTestDelete> UserTestDelete { get; set; }
        public DbSet<OneToTest> OneToTest { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OneToTest>()
                .HasMany(s => s.UserTests)
                .WithOne(c => c.OneToTest)
                .HasForeignKey(s => s.FkOneToTestId);

            //modelBuilder.Entity<UserTest>()
            //    .HasOne(s => s.OneToTest)
            //    .WithMany(g => g.UserTests)
            //    .HasForeignKey(s => s.FkOneToTestId);
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }
    }
}
