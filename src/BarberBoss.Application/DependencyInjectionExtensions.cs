using Microsoft.Extensions.DependencyInjection;
using BarberBoss.Application.AutoMapper;
using BarberBoss.Application.UseCases.Invoicings.Create;

namespace BarberBoss.Application
{
    public static class DependencyInjectionExtensions
    {
        public static void AddApplication(this IServiceCollection service)
        {
            AddUseCases(service);
            AddAutoMapper(service);
        }

        private static void AddAutoMapper(this IServiceCollection service)
        {
            service.AddAutoMapper(typeof(AutoMapping));
        }

        private static void AddUseCases(this IServiceCollection service)
        {
            service.AddScoped<IInvoicingCreateUseCase, InvoicingCreateUseCase>();
        }
    }
}
