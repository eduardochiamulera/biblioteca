using Aplicacao.Queries.Autores;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RelatoriosController(IMediator mediator) : ApiBaseController
	{
		[HttpGet("autores")]
		public async Task<IActionResult> GetByAutores()
		{
			var result = await mediator.Send(new BuscarLivrosPorAutoresReportQuery());

			return Ok(result);
		}
	}
}
