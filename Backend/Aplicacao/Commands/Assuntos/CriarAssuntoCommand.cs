using Core.Entidades;
using Core.Repositorios;
using MediatR;

namespace Aplicacao.Commands.Assuntos
{
    public record CriarAssuntoCommand(string Descricao) : IRequest;

    public class CriarAssuntoCommandHandler( IRepositorioBase<Assunto> assuntoRepository ) : IRequestHandler<CriarAssuntoCommand>
    {
        public async Task Handle( CriarAssuntoCommand request, CancellationToken cancellationToken )
        {
            await assuntoRepository.AdicionarAsync( new Assunto
            {
                Descricao = request.Descricao
            } );

            await assuntoRepository.SalvarAlteracoesAsync();
        }
    }
}
