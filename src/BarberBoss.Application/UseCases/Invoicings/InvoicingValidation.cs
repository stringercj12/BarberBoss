using BarberBoss.Communication.Requests;
using BarberBoss.Exceptions.Messages;
using FluentValidation;

namespace BarberBoss.Application.UseCases.Invoicings
{
    public class InvoicingValidation : AbstractValidator<RequestInvoicingJson>
    {
        public InvoicingValidation()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage(ResourceErrorMessages.TITLE_REQUIRED);
            RuleFor(x => x.Amount).GreaterThan(0).WithMessage(ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO);
            RuleFor(x => x.Date).NotEmpty().WithMessage(ResourceErrorMessages.INVOICINGS_CANNOT_FOR_TH_FUTURE);
            RuleFor(x => x.PaymentType).IsInEnum().WithMessage(ResourceErrorMessages.PAYMENT_TYPE_INVALID);
        }
    }
}
