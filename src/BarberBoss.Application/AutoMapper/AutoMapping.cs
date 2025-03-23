using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;
using BarberBoss.Domain.Entities;
using AutoMapper;

namespace BarberBoss.Application.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            RequestToEntity();
            EntityToResponse();
        }

        private void RequestToEntity()
        {
            CreateMap<RequestInvoicingJson, Invoicing>();
        }
        private void EntityToResponse()
        {
            CreateMap<Invoicing, ResponseInvoicingCreateJson>();
            CreateMap<Invoicing, ResponseInvoicingJson>();
        }


    }
}
