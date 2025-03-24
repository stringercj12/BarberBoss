namespace BarberBoss.Application.UseCases.Invoicings.Reports.Pdf
{
    public interface IGenerateInvoicingsReportExcelUseCase
    {
        Task<byte[]> Execute(DateOnly month);
    }
}
