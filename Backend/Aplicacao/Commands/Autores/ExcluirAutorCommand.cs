using Aplicacao.Responses;
using Core.Entidades;
using Core.Enumeradores;
using Core.Repositorios;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aplicacao.Commands.Autores
{

    public record ExcluirAutorCommand( int Id ) : IRequest<RespostaOperacao>;

    public class ExcluirAssuntoCommandHandler( IRepositorioBase<Autor> assuntoRepositorio, IRepositorioBase<Livro> livroRepositorio ) : IRequestHandler<ExcluirAutorCommand, RespostaOperacao>
    {
        public async Task<RespostaOperacao> Handle( ExcluirAutorCommand request, CancellationToken cancellationToken )
        {
            bool temAlgumLivro = livroRepositorio
                .GetQueryable()
                .Include( x => x.Autores )
                .Any( x => x.Autores.Any( a => a.Id == request.Id ) );

            if( temAlgumLivro )
            {
                return RespostaOperacao.Falha( CodigoErro.ErroInternoServidor, "Autor está vinculado a um livro." );
            }

            await assuntoRepositorio.ExcluirAsync( request.Id );

            await assuntoRepositorio.SalvarAlteracoesAsync();

            return RespostaOperacao.Sucesso;
        }
    }
}
