﻿using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Extensions;
using ClosedXML.Excel;
using BarberBoss.Domain.ResourcesMessages.Reports;
using DocumentFormat.OpenXml.Presentation;

namespace BarberBoss.Application.UseCases.Invoicings.Reports.Excel
{
    public class GenerateInvoicingsReportExcelUseCase : IGenerateInvoicingsReportExcelUseCase
    {
        private const string CURRENCY_SIMBOL = "R$";
        private readonly IInvoicingReadOnlyRepository _repository;

        public GenerateInvoicingsReportExcelUseCase(IInvoicingReadOnlyRepository repository)
        {
            _repository = repository;
        }

        public async Task<byte[]> Execute(DateOnly month)
        {
            var invocings = await _repository.FilterByMonth(month);

            if (invocings.Count == 0)
            {
                return [];
            }


            using var workbook = new XLWorkbook();

            workbook.Author = "Jefferson Ferreira";
            workbook.Style.Font.FontSize = 12;
            workbook.Style.Font.FontName = "BebasNeue-Regular";

            var worksheet = workbook.Worksheets.Add(month.ToString("Y"));

            InsertHeader(worksheet);

            var raw = 2;

            foreach (var invoicing in invocings)
            {
                worksheet.Cell($"A{raw}").Value = invoicing.Title;
                worksheet.Cell($"B{raw}").Value = invoicing.Date;
                worksheet.Cell($"C{raw}").Value = invoicing.PaymentType.PaymentTypeToString();

                worksheet.Cell($"D{raw}").Value = invoicing.Amount;
                worksheet.Cell($"D{raw}").Style.NumberFormat.Format = $"-{CURRENCY_SIMBOL} #,##0.00";

                worksheet.Cell($"E{raw}").Value = invoicing.Description;

                worksheet.Cells($"A{raw}:E{raw}").Style.Font.Bold = false;

                raw++;
            }

            worksheet.Columns().AdjustToContents();

            var file = new MemoryStream();
            workbook.SaveAs(file);

            return file.ToArray();
        }

        private void InsertHeader(IXLWorksheet worksheet)
        {
            worksheet.Cell("A1").Value = ResourceReportGenerationMessages.TITLE.ToLower();
            worksheet.Cell("B1").Value = ResourceReportGenerationMessages.DATE.ToLower();
            worksheet.Cell("C1").Value = ResourceReportGenerationMessages.PAYMENT_TYPE.ToLower();
            worksheet.Cell("D1").Value = ResourceReportGenerationMessages.AMOUNT.ToLower();
            worksheet.Cell("E1").Value = ResourceReportGenerationMessages.DESCRIPTION.ToLower();

            worksheet.Cells("A1:E1").Style.Font.Bold = true;

            worksheet.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.FromHtml("#205858");
            worksheet.Cells("A1:E1").Style.Font.FontColor = XLColor.FromHtml("#ffffff");

            worksheet.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            worksheet.Cell("B1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            worksheet.Cell("C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            worksheet.Cell("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
            worksheet.Cell("E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        }
    }
}
