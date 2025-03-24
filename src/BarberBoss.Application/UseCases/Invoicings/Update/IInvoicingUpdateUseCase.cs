using BarberBoss.Communication.Requests;

namespace BarberBoss.Application.UseCases.Invoicings.Update
{
    public interface IInvoicingUpdateUseCase
    {
        Task Execute(long id, RequestInvoicingJson request);
    }
}