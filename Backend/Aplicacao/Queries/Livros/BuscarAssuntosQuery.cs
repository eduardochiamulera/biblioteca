using Core.Entidades;
using Core.Repositorios;
using MediatR;

namespace Aplicacao.Queries.Livros
{
    public record BuscarLivrosQuery : IRequest<IEnumerable<Livro>>;

    public class BuscarLivrosQueryHandler( IRepositorioBase<Livro> livroRepositorio ) : IRequestHandler<BuscarLivrosQuery, IEnumerable<Livro>>
    {
        public async Task<IEnumerable<Livro>> Handle( BuscarLivrosQuery request, CancellationToken cancellationToken )
        {
            return await livroRepositorio.BuscarTodosAsync();
        }
    }
}
