using Core.Entidades;
using Core.Repositorios;
using MediatR;

namespace Aplicacao.Queries.Assuntos
{
    public record BuscarAssuntoPorCodigoQuery( int Codigo ) : IRequest<Assunto>;

    public class BuscarAssuntoPorCodigoQueryHandler( IRepositorioBase<Assunto> assuntoRepositorio ) : IRequestHandler<BuscarAssuntoPorCodigoQuery, Assunto>
    {
        public async Task<Assunto> Handle( BuscarAssuntoPorCodigoQuery request, CancellationToken cancellationToken )
        {
            return await assuntoRepositorio.BuscarPorCodigoAsync( request.Codigo );
        }
    }
}
