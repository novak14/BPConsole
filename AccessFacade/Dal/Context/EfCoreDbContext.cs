using AccessFacade.Configuration;
using AccessFacade.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccessFacade.Dal.Context
{
    public class EfCoreDbContext : DbContext
    {
        private readonly AccessFacadeOptions options2;

        public EfCoreDbContext(DbContextOptions<EfCoreDbContext> options, IOptions<AccessFacadeOptions> options2) : base(options)
        {
            if (options2 == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            this.options2 = options2.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(options2.connectionString);
        }

        public DbSet<UserTest> UserTest { get; set; }
        public DbSet<UserTestInsert> UserTestInsert { get; set; }
        public DbSet<UserTestUpdate> UserTestUpdate { get; set; }
        public DbSet<UserTestDelete> UserTestDelete { get; set; }
        public DbSet<OneToTest> OneToTest { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTest>()
                .HasOne(s => s.OneToTest)
                .WithMany(g => g.UserTests)
                .HasForeignKey(s => s.FkOneToTestId);
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }
    }
}
