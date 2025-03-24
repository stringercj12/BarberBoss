namespace BarberBoss.Application.UseCases.Invoicings.Delete
{
    public interface IInvoicingDeleteUseCase
    {
        Task Execute(long id);
    }
}