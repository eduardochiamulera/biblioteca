using Aplicacao.Commands.Autores;
using Aplicacao.Commands.Livros;
using Aplicacao.DTOS.Requests;
using Aplicacao.Queries.Autores;
using Aplicacao.Queries.Livros;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class LivrosController( IMediator mediator ) : ApiBaseController
    {
        [HttpGet]
        public async Task<IActionResult> BuscarAsync()
        {
            var result = await mediator.Send( new BuscarLivrosQuery() );

            return Ok( result );
        }

        [HttpGet( "{id:int}" )]
        public async Task<IActionResult> BuscarPorCodigoAsync( int id )
        {
            var result = await mediator.Send( new BuscarLivroPorCodigoQuery( id ) );

            if( result is null )
                return NotFound();

            return Ok( result );
        }

        [HttpPost]
        public async Task<IActionResult> CriarAsync( [FromBody] CriarLivroRequestDto requestDto )
        {
            await mediator.Send( new CriarLivroCommand( requestDto.Titulo, requestDto.Editora, requestDto.Edicao, requestDto.AnoPublicacao, requestDto.AutoresIds, requestDto.AssuntosIds, requestDto.Precos ) );

            return Created();
        }

        [HttpPut( "{id:int}" )]
        public async Task<IActionResult> AtualizarAsync( [FromRoute] int id, [FromBody] AtualizarLivroRequestDto requestDto )
        {
            var result = await mediator.Send( new AtualizarLivroCommand( id, requestDto.Titulo, requestDto.Editora, requestDto.Edicao, requestDto.AnoPublicacao, requestDto.AutoresIds, requestDto.AssuntosIds, requestDto.Precos ) );

            return HandleResponse( result, NoContent() );
        }

        [HttpDelete( "{id:int}" )]
        public async Task<IActionResult> ExcluirAsync( int id )
        {
            var result = await mediator.Send( new ExcluirLivroCommand( id ) );

            return HandleResponse( result, NoContent() );
        }
    }
}
