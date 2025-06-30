using AutoMapper;
using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Entities;
using BarberBoss.Exceptions.ExceptionsBase;

namespace BarberBoss.Application.UseCases.Invoicings.Create
{
    class InvoicingCreateUseCase : IInvoicingCreateUseCase
    {
        private readonly IInvoicingWriteOnlyRepository _invoicingRepository;

        private readonly IMapper _autoMapper;
        private readonly IUnitOfWork _unitOfWork;

        public InvoicingCreateUseCase(IInvoicingWriteOnlyRepository invoicingRepository, IMapper autoMapper, IUnitOfWork unitOfWork)
        {
            _invoicingRepository = invoicingRepository;
            _autoMapper = autoMapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseInvoicingCreateJson> Execute(RequestInvoicingJson request)
        {
            Validate(request);

            var invoicing = _autoMapper.Map<Invoicing>(request);

            await _invoicingRepository.Add(invoicing);
            await _unitOfWork.Commit();

            return _autoMapper.Map<ResponseInvoicingCreateJson>(invoicing);
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
