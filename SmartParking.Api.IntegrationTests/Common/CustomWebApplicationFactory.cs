using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using SmartParking.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace SmartParking.Api.IntegrationTests.Common
{
    /*
    public sealed class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        private string? _connectionString;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");

            builder.ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.Test.json", optional: false);
            });

            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<SmartParkingDbContext>));

                using var provider = services.BuildServiceProvider();
                var configuration = provider.GetRequiredService<IConfiguration>();

                _connectionString = configuration.GetConnectionString("DefaultConnection");

                services.AddDbContext<SmartParkingDbContext>(options =>
                    options.UseNpgsql(_connectionString));
            });
        }

        public async Task ResetDatabaseAsync()
        {
            if (string.IsNullOrWhiteSpace(_connectionString))
                throw new InvalidOperationException("Test connection string is not initialized.");

            var options = new DbContextOptionsBuilder<SmartParkingDbContext>()
                .UseNpgsql(_connectionString)
                .Options;

            await using var dbContext = new SmartParkingDbContext(options);

            await dbContext.Database.ExecuteSqlRawAsync("""
            TRUNCATE TABLE payments, bookings, parkings, drivers, operators
            RESTART IDENTITY CASCADE;
            """);
        }
    }*/


    public sealed class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        private string? _connectionString;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");

            builder.ConfigureAppConfiguration((context, config) =>
            {
                config.SetBasePath(AppContext.BaseDirectory);
                config.AddJsonFile("appsettings.Test.json", optional: false, reloadOnChange: false);
            });

            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<SmartParkingDbContext>));

                using var provider = services.BuildServiceProvider();
                var configuration = provider.GetRequiredService<IConfiguration>();

                _connectionString = configuration.GetConnectionString("DefaultConnection");

                services.AddDbContext<SmartParkingDbContext>(options =>
                    options.UseNpgsql(_connectionString));
            });
        }

        public async Task ResetDatabaseAsync()
        {
            if (string.IsNullOrWhiteSpace(_connectionString))
                throw new InvalidOperationException("Test connection string is not initialized.");

            var options = new DbContextOptionsBuilder<SmartParkingDbContext>()
                .UseNpgsql(_connectionString)
                .Options;

            await using var dbContext = new SmartParkingDbContext(options);

            await dbContext.Database.ExecuteSqlRawAsync("""
            TRUNCATE TABLE payments, bookings, parkings, drivers, operators
            RESTART IDENTITY CASCADE;
            """);
        }
    }
}
