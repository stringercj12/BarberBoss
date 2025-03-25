using System.Net.Mime;
using BarberBoss.Application.UseCases.Invoicings.Reports.Excel;
using BarberBoss.Application.UseCases.Invoicings.Reports.Pdf;
using Microsoft.AspNetCore.Mvc;

namespace BarberBoss.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {

        [HttpGet("excel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetExcel([FromServices] IGenerateInvoicingsReportExcelUseCase useCase, [FromHeader] DateOnly month)
        {
            byte[] file = await useCase.Execute(month);

            if (file.Length > 0)
                return File(file, MediaTypeNames.Application.Octet, "report-excel.xlsx");


            return NoContent();

        }


        [HttpGet("pdf")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetPdf([FromServices] IGenerateInvoicingsReportPdfUseCase useCase, [FromQuery] DateOnly month)
        {
            byte[] file = await useCase.Execute(month);

            if (file.Length > 0)
                return File(file, MediaTypeNames.Application.Pdf, "report-pdf.pdf");


            return NoContent();

        }
    }
}
