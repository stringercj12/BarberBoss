using BarberBoss.Domain.Repositories;
using BarberBoss.Infraestructure.DataAccess;
using BarberBoss.Infraestructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BarberBoss.Infraestructure
{
    public static class DependencyInjectionExtensions
    {

        public static void AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext(configuration);
            AddRepositories(service);
        }
        public static void AddDbContext(this IServiceCollection service, IConfiguration configuration)
        {

            var connectionString = configuration.GetConnectionString("Connection");

            var version = new Version(8, 0, 39);
            var serverVersion = new MySqlServerVersion(version);

            service.AddDbContext<BarberBossDbContext>(config => config.UseMySql(connectionString, serverVersion));
        }

        public static void AddRepositories(IServiceCollection service)
        {
            service.AddScoped<IInvoicingRepository, InvoicingRepository>();
        }
        
    }
}
