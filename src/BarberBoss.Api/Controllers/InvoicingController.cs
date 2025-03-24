using Microsoft.AspNetCore.Mvc;
using BarberBoss.Communication.Requests;
using BarberBoss.Application.UseCases.Invoicings.Create;
using BarberBoss.Communication.Responses;
using BarberBoss.Application.UseCases.Invoicings.GetAll;
using BarberBoss.Application.UseCases.Invoicings.GetById;
using BarberBoss.Application.UseCases.Invoicings.Update;
using BarberBoss.Application.UseCases.Invoicings.Delete;

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

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseInvoicingJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetInvoicingById([FromServices] IInvoicingGetByIdUseCase useCase, long id)
    {
        var response = await useCase.Execute(id);

        return Ok(response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateInvoicing(
        [FromServices] IInvoicingUpdateUseCase useCase,
        [FromBody] RequestInvoicingJson request,
        [FromRoute] long id)
    {
        await useCase.Execute(id, request);

        return NoContent();

    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteInvoicing(
        [FromServices] IInvoicingDeleteUseCase useCase,
        [FromRoute] long id)
    {
        await useCase.Execute(id);

        return NoContent();

    }
}
