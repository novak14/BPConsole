using AccessFacade.Configuration;
using AccessFacade.Dal.Entities;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
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

        public EfCoreDbContext(DbContextOptions<EfCoreDbContext> options) : base(options)
        {
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

        public class ApplicationContextDbFactory : IDesignTimeDbContextFactory<EfCoreDbContext>
        {
            EfCoreDbContext IDesignTimeDbContextFactory<EfCoreDbContext>.CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<EfCoreDbContext>();
                optionsBuilder.UseSqlServer<EfCoreDbContext>("Server=localhost;Database=BakalarskaPrace;Trusted_Connection=True;");

                return new EfCoreDbContext(optionsBuilder.Options);
            }
        }
    }
}
