using AutoMapper;
using BarberBoss.Communication.Responses;
using BarberBoss.Domain.Repositories;

namespace BarberBoss.Application.UseCases.Invoicings.GetAll
{
    class InvoicingGetAllUseCase : IInvoicingGetAllUseCase
    {

        private readonly IInvoicingRepository _invoicingRepository;
        private readonly IMapper _mapper;

        public InvoicingGetAllUseCase(IInvoicingRepository invoicingRepository, IMapper mapper)
        {
            _invoicingRepository = invoicingRepository;
            _mapper = mapper;
        }

        public async Task<ResponseInvoicingsJson> Execute()
        {
            var invoicings = await _invoicingRepository.GetAll();

            return new ResponseInvoicingsJson
            {
                Invoicings = _mapper.Map<List<ResponseInvoicingJson>>(invoicings),
            };
        }
    }
}
