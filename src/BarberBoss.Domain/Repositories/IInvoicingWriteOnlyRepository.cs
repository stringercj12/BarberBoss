using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories
{
    public interface IInvoicingWriteOnlyRepository
    {
        Task Add(Invoicing invoicing);
        Task<bool> Delete(long id);
    }
}
