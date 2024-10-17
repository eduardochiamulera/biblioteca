using Aplicacao.Responses;
using Core.Entidades;
using Core.Enumeradores;
using Core.Repositorios;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aplicacao.Commands.Livros
{
    public record AtualizarPrecoLivroCommand( int LivroId, decimal Preco, FormaCompra FormaCompra ) : IRequest<RespostaOperacao>;

    public class UpdatePrecoLivroCommandHandler( IRepositorioBase<Livro> livroRepositorio ) : IRequestHandler<AtualizarPrecoLivroCommand, RespostaOperacao>
    {
        public async Task<RespostaOperacao> Handle( AtualizarPrecoLivroCommand request, CancellationToken cancellationToken )
        {
            var livro = await livroRepositorio
                .GetQueryable()
                .Include( x => x.Precos )
                .FirstOrDefaultAsync( x => x.Codigo == request.LivroId );

            if( livro == null )
            {
                return RespostaOperacao.Falha( CodigoErro.NaoLocalizado );
            }

            livro.SetPreco( request.Preco, request.FormaCompra );

            livroRepositorio.Atualizar( livro );
            await livroRepositorio.SalvarAlteracoesAsync();

            return RespostaOperacao.Sucesso;
        }
    }
}
