using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;

namespace BarberBoss.Application.UseCases.Invoicings.Create
{
    public interface IInvoicingCreateUseCase
    {
        Task<ResponseInvoicingCreateJson> Execute(RequestInvoicingJson request);
    }
}
