using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories
{
    public interface IInvoicingRepository
    {
        void Add(Invoicing invoicing);
    }
}
