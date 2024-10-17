using Aplicacao.DTOS.Requests;
using Core.Entidades;
using Core.Repositorios;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aplicacao.Commands.Livros
{
    public record CriarLivroCommand( string Titulo, string Editora, int Edicao, string AnoPublicacao, IEnumerable<int> AutoresIds, IEnumerable<int> AssuntosIds, IEnumerable<LivroPrecoRequestDto> Precos ) : IRequest;

    public class CriarLivroCommandHandler( IRepositorioBase<Livro> livroRepositorio, IRepositorioBase<Autor> autorRepositorio, IRepositorioBase<Assunto> assuntoRepositorio ) : IRequestHandler<CriarLivroCommand>
    {
        public async Task Handle( CriarLivroCommand request, CancellationToken cancellationToken )
        {
            var autores = await autorRepositorio
                .GetQueryable()
                .Where( a => request.AutoresIds.Contains( a.Id ) )
                .ToListAsync();

            var assuntos = await assuntoRepositorio
                .GetQueryable()
                .Where( a => request.AssuntosIds.Contains( a.Id ) )
                .ToListAsync();

            var livro = new Livro
            {
                AnoPublicacao = request.AnoPublicacao,
                Edicao = request.Edicao,
                Editora = request.Editora,
                Titulo = request.Titulo,
                Autores = autores,
                Assuntos = assuntos
            };

            foreach( var preco in request.Precos )
            {
                livro.SetPreco( preco.Preco, preco.FormaCompra );
            }

            await livroRepositorio.AdicionarAsync( livro );

            await autorRepositorio.SalvarAlteracoesAsync();
        }
    }
}
