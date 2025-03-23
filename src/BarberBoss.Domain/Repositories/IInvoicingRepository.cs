using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories
{
    public interface IInvoicingRepository
    {
        Task Add(Invoicing invoicing);
        Task<List<Invoicing>> GetAll();
    }
}
