using Microsoft.EntityFrameworkCore;
using Trail_Tracker_App.Data;
using Microsoft.AspNetCore.Identity;
using Trail_Tracker_App.Entities;

namespace Trail_Tracker_App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var authConnection = builder.Configuration.GetConnectionString("TrailConnection") ?? throw new InvalidOperationException("Connection string 'TrailConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySQL(authConnection));

            var mountConnection = builder.Configuration.GetConnectionString("TrailConnection") ?? throw new InvalidOperationException("Connection string 'TrailConnection' not found.");
            builder.Services.AddDbContext<MountaintrailsContext>(options => options.UseMySQL(authConnection));

            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();

            // Add services to the container.
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
