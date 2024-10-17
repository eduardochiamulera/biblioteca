using Aplicacao.DTOS.Requests;
using Aplicacao.Responses;
using Core.Entidades;
using Core.Repositorios;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aplicacao.Commands.Livros
{
    public record AtualizarLivroCommand( int Codigo, string Titulo, string Editora, int Edicao, string AnoPublicacao, IEnumerable<int> AutoresIds, IEnumerable<int> AssuntosIds, IEnumerable<LivroPrecoRequestDto> Precos ) : IRequest<RespostaOperacao>;
    public class AtualizarAutorCommandHandler( IRepositorioBase<Livro> livroRepositorio, IRepositorioBase<Autor> autorRepositorio, IRepositorioBase<Assunto> assuntoRepositorio ) : IRequestHandler<AtualizarLivroCommand, RespostaOperacao>
    {
        public async Task<RespostaOperacao> Handle( AtualizarLivroCommand request, CancellationToken cancellationToken )
        {
            var livro = await livroRepositorio
                .GetQueryable()
                .Include( x => x.Autores )
                .Include( x => x.Assuntos )
                .Include( x => x.Precos )
                .FirstOrDefaultAsync( x => x.Codigo == request.Codigo );

            if( livro is null )
                return RespostaOperacao.Falha( CodigoErro.NaoLocalizado );

            var autores = await autorRepositorio
                .GetQueryable()
                .Where( a => request.AutoresIds.Contains( a.Id ) )
                .ToListAsync();

            var assuntos = await assuntoRepositorio
                .GetQueryable()
                .Where( a => request.AssuntosIds.Contains( a.Id ) )
                .ToListAsync();

            livro.AnoPublicacao = request.AnoPublicacao;
            livro.Edicao = request.Edicao;
            livro.Editora = request.Editora;
            livro.Titulo = request.Titulo;

            livro.Autores = autores;
            livro.Assuntos = assuntos;

            foreach( var preco in request.Precos )
            {
                livro.SetPreco( preco.Preco, preco.FormaCompra );
            }

            livroRepositorio.Atualizar( livro );

            await livroRepositorio.SalvarAlteracoesAsync();

            return RespostaOperacao.Sucesso;
        }
    }
}
