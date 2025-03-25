using Microsoft.Extensions.DependencyInjection;
using BarberBoss.Application.AutoMapper;
using BarberBoss.Application.UseCases.Invoicings.Create;
using BarberBoss.Application.UseCases.Invoicings.GetAll;
using BarberBoss.Application.UseCases.Invoicings.GetById;
using BarberBoss.Application.UseCases.Invoicings.Update;
using BarberBoss.Application.UseCases.Invoicings.Delete;
using BarberBoss.Application.UseCases.Invoicings.Reports.Excel;
using BarberBoss.Application.UseCases.Invoicings.Reports.Pdf;

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
            service.AddScoped<IInvoicingGetAllUseCase, InvoicingGetAllUseCase>();
            service.AddScoped<IInvoicingGetByIdUseCase, InvoicingGetByIdUseCase>();
            service.AddScoped<IInvoicingUpdateUseCase, InvoicingUpdateUseCase>();
            service.AddScoped<IInvoicingDeleteUseCase, InvoicingDeleteUseCase>();
            service.AddScoped<IGenerateInvoicingsReportPdfUseCase, GenerateInvoicingsReportPdfUseCase>();
            service.AddScoped<IGenerateInvoicingsReportExcelUseCase, GenerateInvoicingsReportExcelUseCase>();
        }
    }
}
