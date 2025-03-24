namespace BarberBoss.Application.UseCases.Invoicings.Reports.Excel
{
    public interface IGenerateInvoicingsReportExcelUseCase
    {
        Task<byte[]> Execute(DateOnly month);
    }
}