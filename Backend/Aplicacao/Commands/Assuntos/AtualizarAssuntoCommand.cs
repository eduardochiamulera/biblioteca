using Aplicacao.Responses;
using Core.Entidades;
using Core.Repositorios;
using MediatR;

namespace Aplicacao.Commands.Assuntos
{
    public record AtualizarAssuntoCommand( int Id, string Descricao ) : IRequest<RespostaOperacao>;
    public class AtualizarAssuntoCommandHandler( IRepositorioBase<Assunto> assuntoRepositorio ) : IRequestHandler<AtualizarAssuntoCommand, RespostaOperacao>
    {
        public async Task<RespostaOperacao> Handle( AtualizarAssuntoCommand request, CancellationToken cancellationToken )
        {
            var assunto = await assuntoRepositorio.BuscarPorCodigoAsync( request.Id );

            if( assunto == null )
                return RespostaOperacao.Falha( CodigoErro.NaoLocalizado );

            assunto.Descricao = request.Descricao;

            assuntoRepositorio.Atualizar( assunto );

            await assuntoRepositorio.SalvarAlteracoesAsync();

            return RespostaOperacao.Sucesso;
        }
    }
}
