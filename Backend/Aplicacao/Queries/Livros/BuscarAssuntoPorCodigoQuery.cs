using Core.Entidades;
using Core.Repositorios;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aplicacao.Queries.Livros
{
    public record BuscarLivroPorCodigoQuery( int Codigo ) : IRequest<Livro>;

    public class BuscarAssuntoPorCodigoQueryHandler( IRepositorioBase<Livro> livroRepositorio ) : IRequestHandler<BuscarLivroPorCodigoQuery, Livro>
    {
        public async Task<Livro> Handle( BuscarLivroPorCodigoQuery request, CancellationToken cancellationToken )
        {
            return await livroRepositorio
                .GetQueryable()
                .Include( x => x.Autores )
                .Include( x => x.Assuntos )
                .Include( x => x.Precos )
                .FirstOrDefaultAsync( x => x.Codigo == request.Codigo, cancellationToken );
        }
    }
}
