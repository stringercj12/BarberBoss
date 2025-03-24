using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infraestructure.DataAccess.Repositories;

public class InvoicingRepository : IInvoicingWriteOnlyRepository, IInvoicingReadOnlyRepository, IInvoicingUpdateOnlyRepository
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

    async Task<Invoicing?> IInvoicingReadOnlyRepository.GetById(long id)
    {
        return await _context.Invoicings.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }
    async Task<Invoicing?> IInvoicingUpdateOnlyRepository.GetById(long id)
    {
        return await _context.Invoicings.FirstOrDefaultAsync(x => x.Id == id);
    }

    public void Update(Invoicing invoicing)
    {
        _context.Invoicings.Update(invoicing);
    }

    public async Task<bool> Delete(long id)
    {
        var invoicing = await _context.Invoicings.FirstOrDefaultAsync(f => f.Id == id);

        if (invoicing is null)
        {
            return false;
        }

        _context.Invoicings.Remove(invoicing);

        return true;
    }

    public Task<List<Invoicing>> FilterByMonth(DateOnly month)
    {

        var startDate = new DateTime(year: month.Year, month: month.Month, day: 1).Date;

        var daysInMonth = DateTime.DaysInMonth(year: month.Year, month: month.Month);
        var endDate = new DateTime(year: month.Year, month: month.Month, day: daysInMonth, hour: 23, minute: 59, second: 59).Date;

        return _context.Invoicings
            .Where(invoicing => invoicing.Date >= startDate && invoicing.Date <= endDate)
            .OrderBy(invoicing => invoicing.Date)
            .ThenBy(invoicing => invoicing.Title)
            .ToListAsync();
    }
}
