using BarberBoss.Communication.Enums;
using BarberBoss.Communication.Requests;
using Bogus;

namespace CommonTestUtilities.Requests
{
    public class RequestRegisterInvoicingJsonBuilder
    {
        public static RequestInvoicingJson Build()
        {
            return new Faker<RequestInvoicingJson>()
                .RuleFor(invoicing => invoicing.Title, faker => faker.Commerce.ProductName())
                .RuleFor(invoicing => invoicing.Description, faker => faker.Commerce.ProductDescription())
                .RuleFor(invoicing => invoicing.Date, faker => faker.Date.Past())
                .RuleFor(invoicing => invoicing.PaymentType, faker => faker.PickRandom<PaymentType>())
                .RuleFor(invoicing => invoicing.Amount, faker => faker.Random.Decimal(min: 1, max: 1000));
        }
    }
}
