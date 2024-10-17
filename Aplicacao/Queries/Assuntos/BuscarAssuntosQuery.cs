using Core.Entidades;
using Core.Repositorios;
using MediatR;

namespace Aplicacao.Queries.Assuntos
{
    public record BuscarAssuntosQuery : IRequest<IEnumerable<Assunto>>;

    public class BuscarAssuntosQueryHandler( IRepositorioBase<Assunto> assuntoRepositorio ) : IRequestHandler<BuscarAssuntosQuery, IEnumerable<Assunto>>
    {
        public async Task<IEnumerable<Assunto>> Handle( BuscarAssuntosQuery request, CancellationToken cancellationToken )
        {
            return await assuntoRepositorio.BuscarTodosAsync();
        }
    }
}
