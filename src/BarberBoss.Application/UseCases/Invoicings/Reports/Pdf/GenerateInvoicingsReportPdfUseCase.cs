using System.Reflection;
using BarberBoss.Application.UseCases.Invoicings.Reports.Pdf.Colors;
using BarberBoss.Application.UseCases.Invoicings.Reports.Pdf.Fonts;
using BarberBoss.Domain.Extensions;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.ResourcesMessages.Reports;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Fonts;

namespace BarberBoss.Application.UseCases.Invoicings.Reports.Pdf
{
    public class GenerateInvoicingsReportPdfUseCase : IGenerateInvoicingsReportExcelUseCase
    {
        private const string CURRENCY_SIMBOL = "$";
        private const int HEIGHT_ROW_INVOICING_TABLE = 25;
        private readonly IInvoicingReadOnlyRepository _repository;

        public GenerateInvoicingsReportPdfUseCase(IInvoicingReadOnlyRepository repository)
        {
            _repository = repository;

            GlobalFontSettings.FontResolver = new InvoicingsReportFontResolver();
        }

        public async Task<byte[]> Execute(DateOnly month)
        {
            var invoicings = await _repository.FilterByMonth(month);

            if (invoicings.Count == 0)
            {
                return [];
            }

            var document = CreateDocument(month);
            var page = CreatePage(document);


            CreateHEaderWithProfilePhotoAndName(page);

            var totalInvoicing = invoicings.Sum(invoicing => invoicing.Amount);
            CreateTotalSpentSection(page, month, totalInvoicing);


            foreach (var invoicing in invoicings)
            {
                var table = CreateInvoicingTable(page);

                var row = table.AddRow();
                row.Height = HEIGHT_ROW_INVOICING_TABLE;

                AddInvoicingTitle(row.Cells[0], invoicing.Title);
                AddHeaderForAmount(row.Cells[3]);

                row = table.AddRow();
                row.Height = HEIGHT_ROW_INVOICING_TABLE;

                row.Cells[0].AddParagraph(invoicing.Date.ToString("D"));
                SetStyleBaseForInvoicingInformation(row.Cells[0]);
                row.Cells[0].Format.LeftIndent = 20;

                row.Cells[1].AddParagraph(invoicing.Date.ToString("t"));
                SetStyleBaseForInvoicingInformation(row.Cells[1]);

                row.Cells[2].AddParagraph(invoicing.PaymentType.PaymentTypeToString());
                SetStyleBaseForInvoicingInformation(row.Cells[2]);

                AddAmountForInvoicing(row.Cells[3], invoicing.Amount);

                if(string.IsNullOrWhiteSpace(invoicing.Description) == false)
                {
                    var descriptionRow = table.AddRow();
                    descriptionRow.Height = HEIGHT_ROW_INVOICING_TABLE;

                    descriptionRow.Cells[0].AddParagraph(invoicing.Description);
                    descriptionRow.Cells[0].Format.Font = new Font { Name = FontHelper.ROBOTO_REGULAR, Size = 10, Color = ColorsHelper.GRAY };
                    descriptionRow.Cells[0].Shading.Color = ColorsHelper.GREEN_LIGHT;
                    descriptionRow.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                    descriptionRow.Cells[0].MergeRight = 2;
                    descriptionRow.Cells[0].Format.LeftIndent = 20;

                    row.Cells[3].MergeDown = 1;
                }

                AddWhiteSpace(table);
            }

            return RenderDocument(document);

        }

        private Document CreateDocument(DateOnly month)
        {
            var document = new Document();
            document.Info.Title = $"{ResourceReportGenerationMessages.INVOICINGS_FOR} {month.ToString("Y")}";
            document.Info.Author = "Jefferson Ferreira";

            var style = document.Styles["Normal"];
            style.Font.Name = FontHelper.BEBASNEUE_REGULAR;

            return document;
        }

        private Section CreatePage(Document document)
        {
            var section = document.AddSection();
            section.PageSetup = document.DefaultPageSetup.Clone();

            section.PageSetup.PageFormat = PageFormat.A4;
            section.PageSetup.LeftMargin = 40;
            section.PageSetup.RightMargin = 40;
            section.PageSetup.TopMargin = 80;
            section.PageSetup.BottomMargin = 80;

            return section;
        }

        private void CreateHEaderWithProfilePhotoAndName(Section page)
        {
            var table = page.AddTable();
            table.AddColumn();
            table.AddColumn("300");

            var row = table.AddRow();

            var assembly = Assembly.GetExecutingAssembly();
            var directoryName = Path.GetDirectoryName(assembly.Location);
            var pathFile = Path.Combine(directoryName!, "Logo", "logo.png");


            row.Cells[0].AddImage(pathFile).Height = 30;

            row.Cells[1].AddParagraph("Hey, Jefferson Ferreira");
            row.Cells[1].Format.Font = new Font { Name = FontHelper.BEBASNEUE_REGULAR, Size = 16 };
            row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
        }

        private void CreateTotalSpentSection(Section page, DateOnly month, decimal totalInvoicing)
        {
            var paragraph = page.AddParagraph();
            paragraph.Format.SpaceBefore = "40";
            paragraph.Format.SpaceAfter = "40";

            var title = string.Format(ResourceReportGenerationMessages.TOTAL_SPENT_IN, month.ToString("Y"));

            paragraph.AddFormattedText(title, new Font { Name = FontHelper.ROBOTO_MEDIUM, Size = 15 });

            paragraph.AddLineBreak();

            paragraph.AddFormattedText($"{totalInvoicing} {CURRENCY_SIMBOL}", new Font { Name = FontHelper.BEBASNEUE_REGULAR, Size = 50 });
        }

        private Table CreateInvoicingTable(Section page)
        {
            var table = page.AddTable();

            table.AddColumn("195").Format.Alignment = ParagraphAlignment.Left;
            table.AddColumn("80").Format.Alignment = ParagraphAlignment.Center;
            table.AddColumn("120").Format.Alignment = ParagraphAlignment.Center;
            table.AddColumn("120").Format.Alignment = ParagraphAlignment.Right;

            return table;
        }

        private void AddInvoicingTitle(Cell cell, string title)
        {
            cell.AddParagraph(title);
            cell.Format.Font = new Font { Name = FontHelper.BEBASNEUE_REGULAR, Size = 14, Color = ColorsHelper.BLACK };
            cell.Shading.Color = ColorsHelper.RED_LIGHT;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.MergeRight = 2;
            cell.Format.LeftIndent = 20;
        }

        private void AddHeaderForAmount(Cell cell)
        {
            cell.AddParagraph(ResourceReportGenerationMessages.AMOUNT);
            cell.Format.Font = new Font { Name = FontHelper.BEBASNEUE_REGULAR, Size = 14, Color = ColorsHelper.WHITE };
            cell.Shading.Color = ColorsHelper.RED_DARK;
            cell.VerticalAlignment = VerticalAlignment.Center;
        }

        private void SetStyleBaseForInvoicingInformation(Cell cell)
        {
            cell.Format.Font = new Font { Name = FontHelper.ROBOTO_REGULAR, Size = 12, Color = ColorsHelper.BLACK };
            cell.Shading.Color = ColorsHelper.GREEN_DARK;
            cell.VerticalAlignment = VerticalAlignment.Center;
        }

        private void AddAmountForInvoicing(Cell cell, decimal amount)
        {
            cell.AddParagraph($"-{amount} {CURRENCY_SIMBOL}");
            cell.Format.Font = new Font { Name = FontHelper.ROBOTO_REGULAR, Size = 14, Color = ColorsHelper.BLACK };
            cell.Shading.Color = ColorsHelper.WHITE;
            cell.VerticalAlignment = VerticalAlignment.Center;
        }

        private void AddWhiteSpace(Table table)
        {
           var row = table.AddRow();
            row.Height = 30;
            row.Borders.Visible = false;
        }

        private byte[] RenderDocument(Document document)
        {
            var renderer = new PdfDocumentRenderer
            {
                Document = document,
            };

            renderer.RenderDocument();

            using var file = new MemoryStream();
            renderer.PdfDocument.Save(file);

            return file.ToArray();
        }
    }
}
