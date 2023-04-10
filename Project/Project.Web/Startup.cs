using DocumentFormat.OpenXml.Office2010.PowerPoint;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Project.Domain;
using Project.Domain.Identity;
using Project.Repository;
using Project.Repository.Implementation;
using Project.Repository.Interface;
using Project.Service.Implementation;
using Project.Service.Interface;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Web
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(
					Configuration.GetConnectionString("DefaultConnection")));

			services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
				.AddRoles<IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>();

			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
			services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
			services.AddScoped(typeof(IOrderRepository), typeof(OrderRepository));

			services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));

			services.AddTransient<IProductService, Service.Implementation.ProductService>();
			services.AddTransient<IShoppingCartService, ShoppingCartService>();
			services.AddTransient<IOrderService, Service.Implementation.OrderService>();
			services.AddTransient<IServiceDeviceService, Service.Implementation.ServiceDeviceService>();


			services.AddControllersWithViews().AddRazorRuntimeCompilation();
			services.AddRazorPages();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			StripeConfiguration.SetApiKey(Configuration.GetSection("Stripe")["SecretKey"]);
			app.UseDeveloperExceptionPage();
			app.UseMigrationsEndPoint();
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			using (var scope = app.ApplicationServices.CreateScope())
			{
				var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
				dbContext.Database.Migrate();
			}

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
				endpoints.MapRazorPages();
			});
		}
	}
}
