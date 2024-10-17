using Aplicacao.Responses;
using Core.Entidades;
using Core.Repositorios;
using MediatR;

namespace Aplicacao.Commands.Livros
{

    public record ExcluirLivroCommand( int Id ) : IRequest<RespostaOperacao>;

    public class ExcluirLivroCommandHandler( IRepositorioBase<Livro> livroRepositorio ) : IRequestHandler<ExcluirLivroCommand, RespostaOperacao>
    {
        public async Task<RespostaOperacao> Handle( ExcluirLivroCommand request, CancellationToken cancellationToken )
        {
            await livroRepositorio.ExcluirAsync( request.Id );

            await livroRepositorio.SalvarAlteracoesAsync();

            return RespostaOperacao.Sucesso;
        }
    }
}
