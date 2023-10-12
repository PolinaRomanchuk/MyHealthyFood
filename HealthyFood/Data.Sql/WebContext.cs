using Data.Interface.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Sql
{
    public class WebContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<Manufacturer> Manufacturer { get; set; }
        
        public DbSet<StoreItem> StoreItems { get; set; }

        public WebContext() { }

        public WebContext(DbContextOptions<WebContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
               .HasMany(x => x.Products)
               .WithOne(x => x.Customer)
               .IsRequired(false);
            
            modelBuilder.Entity<Manufacturer>()
                .HasMany(x => x.StoreItems)
                .WithOne(x => x.Manufacturer);

            modelBuilder.Entity<StoreItem>()
                .HasMany(x => x.Users)
                .WithMany(x => x.StoreItems);

			base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=MyHealthyFood;Trusted_Connection=True;";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}