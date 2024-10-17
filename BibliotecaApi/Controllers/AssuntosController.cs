using Aplicacao.Commands.Assuntos;
using Aplicacao.DTOS.Requests;
using Aplicacao.Queries.Assuntos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class AssuntosController( IMediator mediator ) : ApiBaseController
    {
        [HttpGet]
        public async Task<IActionResult> BuscarAsync()
        {
            var result = await mediator.Send( new BuscarAssuntosQuery() );

            return Ok( result );
        }

        [HttpGet( "{id:int}" )]
        public async Task<IActionResult> BuscarPorCodigoAsync( int id )
        {
            var result = await mediator.Send( new BuscarAssuntoPorCodigoQuery( id ) );

            if( result is null )
                return NotFound();

            return Ok( result );
        }

        [HttpPost]
        public async Task<IActionResult> CriarAsync( [FromBody] CriarAssuntoRequestDto requestDto )
        {
            await mediator.Send( new CriarAssuntoCommand( requestDto.Descricao ) );

            return Created();
        }

        [HttpPut( "{id:int}" )]
        public async Task<IActionResult> AtualizarAsync( [FromRoute] int id, [FromBody] AtualizarAssuntoRequestDto requestDto )
        {
            var result = await mediator.Send( new AtualizarAssuntoCommand( id, requestDto.Descricao ) );

            return HandleResponse( result, NoContent() );
        }

        [HttpDelete( "{id:int}" )]
        public async Task<IActionResult> ExcluirAsync( int id )
        {
            var result = await mediator.Send( new ExcluirAssuntoCommand( id ) );

            return HandleResponse( result, NoContent() );
        }
    }
}
