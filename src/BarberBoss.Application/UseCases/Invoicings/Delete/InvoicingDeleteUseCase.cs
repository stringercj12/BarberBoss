
using BarberBoss.Domain.Repositories;
using BarberBoss.Exceptions.ExceptionsBase;
using BarberBoss.Exceptions.Messages;

namespace BarberBoss.Application.UseCases.Invoicings.Delete
{
    public class InvoicingDeleteUseCase : IInvoicingDeleteUseCase
    {
        private readonly IInvoicingWriteOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public InvoicingDeleteUseCase(IInvoicingWriteOnlyRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(long id)
        {
            var result = await _repository.Delete(id);

            if (result == false)
            {
                throw new NotFoundException(ResourceErrorMessages.INVOINCIGS_NOT_FOUND);
            }

            await _unitOfWork.Commit();
        }
    }
}
