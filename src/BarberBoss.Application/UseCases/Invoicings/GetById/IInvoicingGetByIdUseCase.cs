using BarberBoss.Communication.Responses;

namespace BarberBoss.Application.UseCases.Invoicings.GetById
{
    public interface IInvoicingGetByIdUseCase
    {
        Task<ResponseInvoicingJson?> Execute(long id);
    }
}
