using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.Domain.DomainModels;
using Project.Domain.Identity;
using System;

namespace Project.Repository
{
	public class ApplicationDbContext : IdentityDbContext<User>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public virtual DbSet<Product> Products { get; set; }
		public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
		public virtual DbSet<ProductInShoppingCart> ProductInShoppingCarts { get; set; }
		public virtual DbSet<ProductInOrder> ProductInOrders { get; set; }
		public virtual DbSet<Order> Orders { get; set; }
		public virtual DbSet<ServiceDevice> ServiceDevices { get; set; }
		public virtual DbSet<DevicesOfUser> DeviceOfUser { get; set; }



		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<Product>()
				.Property(z => z.Id)
				.ValueGeneratedOnAdd();

			builder.Entity<ServiceDevice>()
				.Property(z => z.Id)
				.ValueGeneratedOnAdd();

			builder.Entity<ShoppingCart>()
				.Property(z => z.Id)
				.ValueGeneratedOnAdd();

			builder.Entity<ShoppingCart>()
				.HasOne<User>(z => z.user)
				.WithOne(z => z.UserCart)
				.HasForeignKey<ShoppingCart>(z => z.userId);

			builder.Entity<DevicesOfUser>()
				.HasOne(z => z.Device)
				.WithMany(z => z.DevicesOfUser)
				.HasForeignKey(z => z.DeviceId);

			builder.Entity<DevicesOfUser>()
				.HasOne(z => z.User)
				.WithMany(z => z.DevicesOfUser)
				.HasForeignKey(z => z.UserId);


			//Gabim jan keto te ngatruara jan mes veti
			builder.Entity<ProductInShoppingCart>()
				.HasOne(z => z.Product)
				.WithMany(z => z.ProductInShoppingCarts)
				.HasForeignKey(z => z.ShoppingCartId);

			builder.Entity<ProductInShoppingCart>()
				.HasOne(z => z.ShoppingCart)
				.WithMany(z => z.ProductInShoppingCarts)
				.HasForeignKey(z => z.ProductId);

			builder.Entity<ProductInOrder>()
				.HasOne(z => z.OrderedProduct)
				.WithMany(z => z.ProductInOrders)
				.HasForeignKey(z => z.OrderId);

			builder.Entity<ProductInOrder>()
				.HasOne(z => z.UserOrder)
				.WithMany(z => z.ProductInOrders)
				.HasForeignKey(z => z.ProductId);
		}
	}
}
