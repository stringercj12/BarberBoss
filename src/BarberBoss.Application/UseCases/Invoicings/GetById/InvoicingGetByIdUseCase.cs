using AutoMapper;
using BarberBoss.Communication.Responses;
using BarberBoss.Domain.Repositories;

namespace BarberBoss.Application.UseCases.Invoicings.GetById
{
    public class InvoicingGetByIdUseCase : IInvoicingGetByIdUseCase
    {
        private readonly IInvoicingReadOnlyRepository _invoicingRespository;
        private readonly IMapper _mapper;

        public InvoicingGetByIdUseCase(IInvoicingReadOnlyRepository invoicingRespository, IMapper mapper)
        {
            _invoicingRespository = invoicingRespository;
            _mapper = mapper;
        }

        public async Task<ResponseInvoicingJson?> Execute(long id)
        {
            var result = await _invoicingRespository.GetById(id);

            return _mapper.Map<ResponseInvoicingJson>(result);
        }
    }
}
