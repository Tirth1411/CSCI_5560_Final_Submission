using ROS.Implement.Repository;
using ROS.Implement;
using Microsoft.EntityFrameworkCore;

namespace ROS.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            #region Connection string Declaration
            var connection = builder.Configuration.GetConnectionString("DafaultConnection");

            //builder.Services.AddDbContext<HW6DbContext>(options => options.UseMySql(connection));
            builder.Services.AddDbContext<HW6DbContext>(options =>
            options.UseMySql(connection, new MySqlServerVersion(new Version(8, 0, 23)))); // Replace with your MySQL version
            #endregion



            builder.Services.AddScoped<IAuthRepository, AuthRepository>();
            builder.Services.AddScoped<IHW6Repository, HW6Repository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=HW6}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
