using MyEcomWebApplication.Application.DataAccess;
using Microsoft.EntityFrameworkCore;
using MyEcomWebApplication.Application.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace MyEcomWebApplication
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void Configure(IApplicationBuilder app)
        {

        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<DeliveryService>();

            // Register services
            services.AddScoped<IDeliveryService, DeliveryService>();
            services.AddScoped<IDeliveryRepository, DeliveryRepository>();

            // Retrieve connection string from appsettings.json
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            // Register DataAccess with the connection string
            services.AddScoped<DataAccess>(provider => new DataAccess(connectionString));

        }


    }
}
