using Microsoft.AspNetCore.Mvc;
using BarberBoss.Communication.Requests;
using BarberBoss.Application.UseCases.Invoicings.Create;
using BarberBoss.Communication.Responses;

namespace BarberBoss.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class InvoicingController : ControllerBase
{

    [HttpPost]
    [ProducesResponseType(typeof(ResponseInovoicingCreateJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson),StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateInvoicing([FromServices] IInvoicingCreateUseCase useCase, RequestInvoicingJson request)
    {

        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }
}
