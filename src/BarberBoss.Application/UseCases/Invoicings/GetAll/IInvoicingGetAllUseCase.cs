using BarberBoss.Communication.Responses;

namespace BarberBoss.Application.UseCases.Invoicings.GetAll
{
    public interface IInvoicingGetAllUseCase
    {
        Task<ResponseInvoicingsJson> Execute();
    }
}