using Core.Entidades;
using Core.Repositorios;
using MediatR;

namespace Aplicacao.Queries.Autores
{
    public record BuscarAutoresQuery : IRequest<IEnumerable<Autor>>;
    public class BuscarAutoresQueryHandler( IRepositorioBase<Autor> autorRepositorio ) : IRequestHandler<BuscarAutoresQuery, IEnumerable<Autor>>
    {
        public Task<IEnumerable<Autor>> Handle( BuscarAutoresQuery request, CancellationToken cancellationToken )
        {
            return autorRepositorio.BuscarTodosAsync();
        }
    }
}
