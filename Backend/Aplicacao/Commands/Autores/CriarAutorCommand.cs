using Core.Entidades;
using Core.Repositorios;
using MediatR;

namespace Aplicacao.Commands.Autores
{
    public record CriarAutorCommand(string Nome) : IRequest;

    public class CriarAutorCommandHandler( IRepositorioBase<Autor> autorRepositorio ) : IRequestHandler<CriarAutorCommand>
    {
        public async Task Handle( CriarAutorCommand request, CancellationToken cancellationToken )
        {
            await autorRepositorio.AdicionarAsync( new Autor
            {
                Nome = request.Nome
            } );

            await autorRepositorio.SalvarAlteracoesAsync();
        }
    }
}
