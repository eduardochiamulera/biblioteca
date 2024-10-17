using Aplicacao.Responses;
using Core.Entidades;
using Core.Repositorios;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aplicacao.Commands.Assuntos
{

    public record ExcluirAssuntoCommand( int Id ) : IRequest<RespostaOperacao>;

    public class ExcluirAssuntoCommandHandler( IRepositorioBase<Assunto> assuntoRepositorio, IRepositorioBase<Livro> livroRepositorio ) : IRequestHandler<ExcluirAssuntoCommand, RespostaOperacao>
    {
        public async Task<RespostaOperacao> Handle( ExcluirAssuntoCommand request, CancellationToken cancellationToken )
        {
            var temAlgumAssunto = livroRepositorio
                .GetQueryable()
                .Include( x => x.Assuntos )
                .Any( x => x.Assuntos.Any( a => a.Id == request.Id ) );

            if( temAlgumAssunto )
            {
                return RespostaOperacao.Falha( CodigoErro.ErroInternoServidor, "Assunto está vinculado a um livro." );
            }

            await assuntoRepositorio.ExcluirAsync( request.Id );

            await assuntoRepositorio.SalvarAlteracoesAsync();

            return RespostaOperacao.Sucesso;
        }
    }
}
