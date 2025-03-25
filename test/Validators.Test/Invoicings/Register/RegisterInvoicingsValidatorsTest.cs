using BarberBoss.Application.UseCases.Invoicings;
using BarberBoss.Communication.Enums;
using BarberBoss.Exceptions.Messages;
using CommonTestUtilities.Requests;
using Shouldly;

namespace Validators.Test.Invoicings.Register
{
    public class RegisterInvoicingsValidatorsTest
    {

        [Fact]
        public void Success()
        {
            var validator = new InvoicingValidation();
            var request = RequestRegisterInvoicingJsonBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.ShouldNotBe(false);
        }

        [Theory]
        [InlineData("")]
        [InlineData("      ")]
        [InlineData(null)]
        public void Error_Title_Empty(string title)
        {
            var validator = new InvoicingValidation();
            var request = RequestRegisterInvoicingJsonBuilder.Build();
            request.Title = title;

            var result = validator.Validate(request);

            result.IsValid.ShouldBe(false);

            result.Errors.ShouldSatisfyAllConditions(
               errors => errors.ShouldHaveSingleItem(),
               errors => errors.ShouldNotBeNull(),
               errors => errors.ShouldContain(error => error.ErrorMessage.Equals(ResourceErrorMessages.TITLE_REQUIRED))
            );
        }


        [Fact]
        public void Error_Date_Future()
        {
            var validator = new InvoicingValidation();
            var request = RequestRegisterInvoicingJsonBuilder.Build();
            request.Date = DateTime.UtcNow.AddDays(1);

            var result = validator.Validate(request);

            result.IsValid.ShouldBe(false);

            result.Errors.ShouldSatisfyAllConditions(
               errors => errors.ShouldHaveSingleItem(),
               errors => errors.ShouldNotBeNull(),
               errors => errors.ShouldContain(error => error.ErrorMessage.Equals(ResourceErrorMessages.INVOICINGS_CANNOT_FOR_TH_FUTURE))
            );
        }


        [Fact]
        public void Error_Payment_Type_Invalid()
        {
            var validator = new InvoicingValidation();
            var request = RequestRegisterInvoicingJsonBuilder.Build();
            request.PaymentType = (PaymentType)700;

            var result = validator.Validate(request);

            result.IsValid.ShouldBe(false);

            result.Errors.ShouldSatisfyAllConditions(
               errors => errors.ShouldHaveSingleItem(),
               errors => errors.ShouldNotBeNull(),
               errors => errors.ShouldContain(error => error.ErrorMessage.Equals(ResourceErrorMessages.PAYMENT_TYPE_INVALID))
            );
        }


        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(-7)]
        public void Error_Amount_Invalid(decimal amount)
        {
            var validator = new InvoicingValidation();
            var request = RequestRegisterInvoicingJsonBuilder.Build();
            request.Amount = amount;

            var result = validator.Validate(request);

            result.IsValid.ShouldBe(false);

            result.Errors.ShouldSatisfyAllConditions(
               errors => errors.ShouldHaveSingleItem(),
               errors => errors.ShouldNotBeNull(),
               errors => errors.ShouldContain(error => error.ErrorMessage.Equals(ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO))
            );
        }
    }
}
