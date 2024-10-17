using Core.Entidades;
using Core.Repositorios;
using MediatR;

namespace Aplicacao.Queries.Autores
{
    public record BuscarAutorPorCodigoQuery( int Cod ) : IRequest<Autor>;

    public class BuscarAutorPorCodigoQueryHandler( IRepositorioBase<Autor> autorRepositorio ) : IRequestHandler<BuscarAutorPorCodigoQuery, Autor>
    {
        public Task<Autor> Handle( BuscarAutorPorCodigoQuery request, CancellationToken cancellationToken )
        {
            return autorRepositorio.BuscarPorCodigoAsync( request.Cod );
        }
    }

}
