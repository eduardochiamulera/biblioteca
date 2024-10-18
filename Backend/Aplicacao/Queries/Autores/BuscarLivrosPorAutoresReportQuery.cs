using Dapper;
using Infra;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aplicacao.Queries.Autores
{
    public record LivroReportQueryResultado( string Titulo, string Editora, int Edicao, string AnoPublicacao, string Assuntos );
    public record LivrosPorAutorQueryResultado( string Autor, IEnumerable<LivroReportQueryResultado> Livros );
    public record BuscarLivrosPorAutoresReportQuery : IRequest<IEnumerable<LivrosPorAutorQueryResultado>>;

    public class BuscarLivrosGroupedByAutorReportQueryHandler( BibliotecaContexto contexto ) : IRequestHandler<BuscarLivrosPorAutoresReportQuery, IEnumerable<LivrosPorAutorQueryResultado>>
    {
        public async Task<IEnumerable<LivrosPorAutorQueryResultado>> Handle( BuscarLivrosPorAutoresReportQuery request, CancellationToken cancellationToken )
        {
            var sql = @"SELECT A.Cod AS CodigoAutor 
                               ,A.Nome
                               ,L.Titulo
                               ,L.Editora
                               ,L.Edicao
                               ,L.AnoPublicacao
                               ,AST.Descricao,
                               L.CodL AS CodigoLivro 
                        FROM Autor A
                        LEFT JOIN Livro_Autor LA 
                            on LA.Autor_CodAu = A.Cod
                        LEFT JOIN Livro L 
                            on L.CodL = LA.Livro_CodL
                        LEFT JOIN Livro_Assunto LAS 
                            on LAS.Livro_CodL = L.CodL
                        LEFT JOIN Assunto AST 
                            on AST.CodAs = LAS.Assunto_CodAs";

            var result = await contexto.Database.GetDbConnection().QueryAsync<QueryResult>( sql );

            return result.GroupBy( x => x.CodigoAutor ).Select( x => new LivrosPorAutorQueryResultado
            (
                Autor: x.FirstOrDefault()?.Nome,
                Livros: x.Where( l => l.CodigoLivro != 0 ).GroupBy( l => l.CodigoLivro ).Select( l =>
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
            public int CodigoAutor { get; set; }
            public string Descricao { get; set; }
            public string AnoPublicacao { get; set; }
            public string Titulo { get; set; }
            public string Editora { get; set; }
            public int Edicao { get; set; }
            public string Nome { get; set; }
            public int CodigoLivro { get; set; }
        }
    }
}
