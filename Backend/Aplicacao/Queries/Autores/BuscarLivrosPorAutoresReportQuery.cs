using Dapper;
using Infra;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aplicacao.Queries.Autores
{
    public record LivroReportQueryResultado( string Titulo, string Editora, int Edicao, string AnoPublicacao, string Assuntos );
    public record LivrosPorAutorQueryResultado( string Autor, IEnumerable<LivroReportQueryResultado> Livros );
    public record BuscarLivrosPorAutoresReportQuery : IRequest<IEnumerable<LivrosPorAutorQueryResultado>>;

    public class GetLivrosGroupedByAutorReportQueryHandler( BibliotecaContexto contexto ) : IRequestHandler<BuscarLivrosPorAutoresReportQuery, IEnumerable<LivrosPorAutorQueryResultado>>
    {
        public async Task<IEnumerable<LivrosPorAutorQueryResultado>> Handle( BuscarLivrosPorAutoresReportQuery request, CancellationToken cancellationToken )
        {
            var sql = @"SELECT A.Cod AS CodigoAutor, 
                               ,A.Nome
                               ,L.Titulo
                               ,L.Editora
                               ,L.Edicao
                               ,L.AnoPublicacao
                               ,AST.Descricao,
                               L.CodL AS CodigoLivro 
                        FROM Autor A
                        LEFT JOIN LivroAutor LA 
                            on LA.AutorId = A.Cod
                        LEFT JOIN Livro L 
                            on L.Cod = LA.LivroId
                        LEFT JOIN LivroAssunto LAS 
                            on LAS.LivroId = L.Cod
                        LEFT JOIN Assunto AST 
                            on AST.CodAs = LAS.AssuntoId";

            var result = await contexto.Database.GetDbConnection().QueryAsync<QueryResult>( sql );

            return result.GroupBy( x => x.CodAutor ).Select( x => new LivrosPorAutorQueryResultado
            (
                Autor: x.FirstOrDefault()?.Nome,
                Livros: x.Where( l => l.CodLivro != 0 ).GroupBy( l => l.CodLivro ).Select( l =>
                {
                    var firstLivro = l.FirstOrDefault();

                    return new LivroReportQueryResultado(
                    Titulo: firstLivro.Titulo,
                    AnoPublicacao: firstLivro.AnoPublicacao,
                    Edicao: firstLivro.Edicao,
                    Editora: firstLivro.Editora,
                    Assuntos: string.Join( ", ", l.Select( a => a.Descricao ).Distinct().Where( a => !string.IsNullOrEmpty( a ) ) ) );
                } ).ToList()
            ) );
        }

        sealed record QueryResult
        {
            public int CodAutor { get; set; }
            public int Name { get; set; }
            public string Descricao { get; set; }
            public string AnoPublicacao { get; set; }
            public string Titulo { get; set; }
            public string Editora { get; set; }
            public int Edicao { get; set; }
            public string Nome { get; set; }
            public int CodLivro { get; set; }
        }
    }
}
