using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories
{
    public interface IInvoicingReadOnlyRepository
    {
        Task<List<Invoicing>> GetAll();
        Task<Invoicing?> GetById(long id);
    }
}
