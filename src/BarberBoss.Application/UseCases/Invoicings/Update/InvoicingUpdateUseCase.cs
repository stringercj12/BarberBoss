using AutoMapper;
using BarberBoss.Communication.Requests;
using BarberBoss.Domain.Repositories;
using BarberBoss.Exceptions.ExceptionsBase;
using BarberBoss.Exceptions.Messages;

namespace BarberBoss.Application.UseCases.Invoicings.Update
{
    public class InvoicingUpdateUseCase : IInvoicingUpdateUseCase
    {
        private readonly IInvoicingUpdateOnlyRepository _repositoryInvoicing;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InvoicingUpdateUseCase(IInvoicingUpdateOnlyRepository repositoryInvoicing, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repositoryInvoicing = repositoryInvoicing;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(long id, RequestInvoicingJson request)
        {
            Validate(request);

            var invoicing = await _repositoryInvoicing.GetById(id);

            if (invoicing is null)
            {
                throw new NotFoundException(ResourceErrorMessages.INVOINCIGS_NOT_FOUND);
            }

            _mapper.Map(request, invoicing);

            _repositoryInvoicing.Update(invoicing);
            await _unitOfWork.Commit();

        }

        private void Validate(RequestInvoicingJson request)
        {
            var validator = new InvoicingValidation();
            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);

            }
        }

    }
}
