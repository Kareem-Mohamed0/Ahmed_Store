using AhmedStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AhmedStore.ViewModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace AhmedStore.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext()
        {

        }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ShopCategory> ShopCategories { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var Config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var constring = Config.GetSection("DefaultConnectionString").Value;
            optionsBuilder.UseSqlServer(constring);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CartProduct>()
                .Property(Q => Q.Quantity)
                .HasDefaultValue(1);
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);
            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .ValueGeneratedOnAdd();
            // Configure CartProduct entity
            modelBuilder.Entity<CartProduct>()
                .HasKey(cp => new { cp.CartId, cp.ProductId });

            modelBuilder.Entity<CartProduct>()
                .HasOne(cp => cp.Cart)
                .WithMany(c => c.CartProducts)
                .HasForeignKey(cp => cp.CartId)
                .OnDelete(DeleteBehavior.Restrict); // Restrict cascade delete to avoid multiple cascade paths

            modelBuilder.Entity<CartProduct>()
                .HasOne(cp => cp.Product)
                .WithMany(p => p.CartProducts)
                .HasForeignKey(cp => cp.ProductId)
                .OnDelete(DeleteBehavior.Restrict); // Restrict cascade delete to avoid multiple cascade paths

            // Configure Order entity
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Restrict cascade delete to avoid multiple cascade paths

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Shop)
                .WithMany(s => s.Orders)
                .HasForeignKey(o => o.ShopId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Category>()
                .HasOne(o => o.Shop)
                .WithMany(s => s.Categories)
                .HasForeignKey(o => o.ShopId)
                .OnDelete(DeleteBehavior.Cascade); // Restrict cascade delete to avoid multiple cascade paths
            modelBuilder.Entity<Product>()
                .HasOne(o => o.Category)
                .WithMany(s => s.Products)
                .HasForeignKey(o => o.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            /*********************************************/
            modelBuilder.Entity<Cart>()
                .HasMany(c => c.CartProducts)
                .WithOne(p => p.Cart)
                .HasForeignKey(c => c.CartId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>()
                .HasMany(U => U.Orders)
                .WithOne(O => O.User)
                .HasForeignKey(U => U.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<AhmedStore.ViewModels.DisplayUsersVM> DisplayUsersVM { get; set; } = default!;
        public DbSet<AhmedStore.ViewModels.RoleVM> RoleVM { get; set; } = default!;
        public DbSet<AhmedStore.ViewModels.AssignRoleVM> AssignRoleVM { get; set; } = default!;

    }
}
