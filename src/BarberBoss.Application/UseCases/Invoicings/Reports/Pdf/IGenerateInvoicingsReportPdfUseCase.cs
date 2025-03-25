namespace BarberBoss.Application.UseCases.Invoicings.Reports.Pdf
{
    public interface IGenerateInvoicingsReportPdfUseCase
    {
        Task<byte[]> Execute(DateOnly month);
    }
}
