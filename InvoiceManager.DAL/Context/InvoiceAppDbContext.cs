using System.IO;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.Context
{
    public class InvoiceAppDbContext : IdentityDbContext<ApplicationUser>, IDesignTimeDbContextFactory<InvoiceAppDbContext>
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItems> InvoiceItems { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public InvoiceAppDbContext()
        {
        }

        public InvoiceAppDbContext(DbContextOptions<InvoiceAppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<InvoiceItems>().HasKey(c => new { c.InvoiceId, c.ItemId });

            modelBuilder.Entity<ApplicationUser>()
               .HasMany(au => au.Invoices)
               .WithOne(i => i.ApplicationUser)
               .HasForeignKey(i => i.ApplicationUserId)
               .OnDelete(DeleteBehavior.Cascade);

        }

        public InvoiceAppDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@Directory.GetCurrentDirectory() + "/../InvoiceApp/appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<InvoiceAppDbContext>();
            var connectionString = configuration.GetConnectionString("InvoiceAppDbContext");
            builder.UseSqlServer(connectionString);

            return new InvoiceAppDbContext(builder.Options);
        }
    }
}
