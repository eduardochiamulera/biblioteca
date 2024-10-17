using Aplicacao.Responses;
using Core.Entidades;
using Core.Repositorios;
using MediatR;

namespace Aplicacao.Commands.Autores
{
    public record AtualizarAutorCommand( int Id, string Nome ) : IRequest<RespostaOperacao>;
    public class AtualizarAutorCommandHandler( IRepositorioBase<Autor> autorRepositorio ) : IRequestHandler<AtualizarAutorCommand, RespostaOperacao>
    {
        public async Task<RespostaOperacao> Handle( AtualizarAutorCommand request, CancellationToken cancellationToken )
        {
            var autor = await autorRepositorio.BuscarPorCodigoAsync( request.Id );

            if( autor is null )
                return RespostaOperacao.Falha( CodigoErro.NaoLocalizado );

            autor.Nome = request.Nome;

            autorRepositorio.Atualizar( autor );

            await autorRepositorio.SalvarAlteracoesAsync();

            return RespostaOperacao.Sucesso;
        }
    }
}
