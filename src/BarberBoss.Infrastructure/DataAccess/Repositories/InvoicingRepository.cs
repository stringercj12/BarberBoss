using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infraestructure.DataAccess.Repositories;

public class InvoicingRepository : IInvoicingRepository
{

    private readonly BarberBossDbContext _context;

    public InvoicingRepository(BarberBossDbContext context)
    {
        _context = context;
    }

    public async Task Add(Invoicing invoicing)
    {
        await _context.Invoicings.AddAsync(invoicing);
        _context.SaveChanges();
    }

    public async Task<List<Invoicing>> GetAll()
    {
      return await _context.Invoicings.AsNoTracking().ToListAsync();
    }
}