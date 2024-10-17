using Aplicacao.Commands.Autores;
using Aplicacao.DTOS.Requests;
using Aplicacao.Queries.Autores;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class AutoresController( IMediator mediator ) : ApiBaseController
    {
        [HttpGet]
        public async Task<IActionResult> BuscarAsync()
        {
            var result = await mediator.Send( new BuscarAutoresQuery() );

            return Ok( result );
        }

        [HttpGet( "{id:int}" )]
        public async Task<IActionResult> BuscarPorCodigoAsync( int id )
        {
            var result = await mediator.Send( new BuscarAutorPorCodigoQuery( id ) );

            if( result is null )
                return NotFound();

            return Ok( result );
        }

        [HttpPost]
        public async Task<IActionResult> CriarAsync( [FromBody] CriarAutorRequestDto requestDto )
        {
            await mediator.Send( new CriarAutorCommand( requestDto.Nome ) );

            return Created();
        }

        [HttpPut( "{id:int}" )]
        public async Task<IActionResult> AtualizarAsync( [FromRoute] int id, [FromBody] AtualizarAutorRequestDto requestDto )
        {
            var result = await mediator.Send( new AtualizarAutorCommand( id, requestDto.Nome ) );

            return HandleResponse( result, NoContent() );
        }

        [HttpDelete( "{id:int}" )]
        public async Task<IActionResult> ExcluirAsync( int id )
        {
            var result = await mediator.Send( new ExcluirAutorCommand( id ) );

            return HandleResponse( result, NoContent() );
        }
    }
}
