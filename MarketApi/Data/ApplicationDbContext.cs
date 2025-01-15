using MarketApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static MarketApi.Models.FavoriteProducts;

namespace MarketApi.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<FavoriteProduct> FavoriteProducts { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().HasIndex(x => x.Email).IsUnique();
            // Configure many-to-many relationships for FavoriteProducts
            builder.Entity<FavoriteProduct>()
                .HasKey(fp => new { fp.UserId, fp.ProductId });

            builder.Entity<FavoriteProduct>()
                .HasOne(fp => fp.User)
                .WithMany(u => u.FavoriteProducts)
                .HasForeignKey(fp => fp.UserId);

            builder.Entity<FavoriteProduct>()
                .HasOne(fp => fp.Product)
                .WithMany()
                .HasForeignKey(fp => fp.ProductId);

            // Configure many-to-many relationships for CartProducts
            builder.Entity<CartProduct>()
                .HasIndex(x=>x.UserId);

            builder.Entity<CartProduct>()
                .HasKey(cp => new { cp.UserId, cp.ProductId });

            builder.Entity<CartProduct>()
                .HasOne(cp => cp.User)
                .WithMany(u => u.CartProducts)
                .HasForeignKey(cp => cp.UserId);

            builder.Entity<Product>().HasMany(x => x.comments).WithOne(x => x.Product);


            builder.Entity<Order>().HasIndex(x=>x.UserId);

            builder.Entity<Order>().HasMany(x => x.OrderProducts).WithOne(x => x.Order).HasForeignKey(x => x.OrderId);

            builder.Entity<Order>().HasMany(x => x.OrderStates).WithOne(x => x.Order).HasForeignKey(x=>x.OrderId);

            builder.Entity<OrderAddress>()
              .HasOne(x=>x.Order)
              .WithOne(x=>x.OrderAddress)
              .HasForeignKey<OrderAddress>(x => x.OrderId).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<OrderPayment>()
              .HasOne(x => x.Order)
              .WithOne(x => x.OrderPayment)
              .HasForeignKey<OrderPayment>(x => x.OrderId).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<OrderProduct>().HasOne(x => x.Order).WithMany(x => x.OrderProducts).HasForeignKey(x => x.OrderId);
            builder.Entity<OrderProduct>().HasIndex(x => x.OrderId);
            builder.Entity<OrderProduct>().HasKey(x=>x.Id);


            builder.Entity<OrderState>().HasOne(x => x.Order).WithMany(x => x.OrderStates).HasForeignKey(x => x.OrderId);
            builder.Entity<OrderState>().HasIndex(x => x.OrderId);
            builder.Entity<OrderState>().HasKey(x=>x.Id);

            builder.Entity<Category>()
                .HasOne(c => c.Parent)
                .WithMany(c => c.Children)
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Category>().Property(X => X.ParentId).IsRequired(false);

            builder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
