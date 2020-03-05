using System.Data;
using Geonames.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using FluentMigrator.Runner;
using Geonames.Migrations;

namespace Geonames
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
            services.AddControllersWithViews();
            // Read the connection string from appsettings.
            var dbConnectionString = this.Configuration.GetConnectionString("DefaultConnection");
            services.AddFluentMigratorCore()
                .ConfigureRunner(
                    builder => builder
                        .AddPostgres()
                        .WithGlobalConnectionString(dbConnectionString)
                        .ScanIn(typeof(AddGeonamesTable).Assembly).For.Migrations());

            // Inject IDbConnection, with implementation from NpgsqlConnection class.
            services.AddTransient<IDbConnection>((sp) => new NpgsqlConnection(dbConnectionString));
            services.AddTransient<IDatabaseWrapper, DatabaseWrapper>();
            services.AddScoped<IGeonamesProvider, GeonamesProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var runner = serviceScope.ServiceProvider.GetService<IMigrationRunner>();
                // Run the migrations
                runner.MigrateDown(20200304144000);
                runner.MigrateUp();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Geonames}/{action=Index}/{id?}");
            });
        }
    }
}