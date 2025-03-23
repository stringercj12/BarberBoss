using Microsoft.AspNetCore.Mvc;
using BarberBoss.Communication.Requests;
using BarberBoss.Application.UseCases.Invoicings.Create;
using BarberBoss.Communication.Responses;
using BarberBoss.Application.UseCases.Invoicings.GetAll;

namespace BarberBoss.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class InvoicingController : ControllerBase
{

    [HttpPost]
    [ProducesResponseType(typeof(ResponseInvoicingCreateJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateInvoicing([FromServices] IInvoicingCreateUseCase useCase, RequestInvoicingJson request)
    {

        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseInvoicingsJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllInvoicing([FromServices] IInvoicingGetAllUseCase useCase)
    {
        var response = await useCase.Execute();

        if (response.Invoicings.Count != 0)
            return Ok(response);

        return NoContent();
    }
}
