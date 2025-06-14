﻿using BarberBoss.Domain.Enums;
using BarberBoss.Domain.ResourcesMessages.Reports;

namespace BarberBoss.Domain.Extensions
{
    public static class PaymentTypeExtensions
    {
        public static string PaymentTypeToString(this PaymentType paymentType)
        {
            return paymentType switch
            {
                PaymentType.Cash => ResourceReportGenerationMessages.CASH,
                PaymentType.CreditCard => ResourceReportGenerationMessages.CREDIT_CARD,
                PaymentType.DebitCard => ResourceReportGenerationMessages.DEBIT_CARD,
                PaymentType.EletronicTransfer => ResourceReportGenerationMessages.ELETRONIC_TRANSFER,
                _ => string.Empty
            };

        }
    }
}
