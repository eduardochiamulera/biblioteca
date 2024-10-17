using System.ComponentModel.DataAnnotations;
using Core.Enumeradores;

namespace Aplicacao.DTOS.Requests
{
    #region Livro
    public record CriarLivroRequestDto()
    {
        [Required]
        public string Titulo { get; init; } = null!;
        public string Editora { get; init; } = null!;
        public int Edicao { get; init; }
        public string AnoPublicacao { get; init; } = null!;
        public IEnumerable<int> AutoresIds { get; init; } = Enumerable.Empty<int>();
        public IEnumerable<int> AssuntosIds { get; init; } = Enumerable.Empty<int>();
        public IEnumerable<LivroPrecoRequestDto> Precos { get; init; } = Enumerable.Empty<LivroPrecoRequestDto>();
    }

    public record AtualizarLivroRequestDto()
    {
        [Required]
        public string Titulo { get; init; } = null!;
        public string Editora { get; init; } = null!;
        public int Edicao { get; init; }
        public string AnoPublicacao { get; init; } = null!;
        public IEnumerable<int> AutoresIds { get; init; } = Enumerable.Empty<int>();
        public IEnumerable<int> AssuntosIds { get; init; } = Enumerable.Empty<int>();
        public IEnumerable<LivroPrecoRequestDto> Precos { get; init; } = Enumerable.Empty<LivroPrecoRequestDto>();
    }

    public record AtualizarLivroPrecoRequestDto( [Required( ErrorMessage = "Preço é obrigatório." )] decimal Preco, [Required( ErrorMessage = "Forma de Compra é obrigatório." )] FormaCompra FormaCompra );
    public record LivroPrecoRequestDto( decimal Preco, FormaCompra FormaCompra );

    #endregion

    #region Autor
    public record CriarAutorRequestDto( [Required( ErrorMessage = "Nome é obrigatório." )] string Nome );
    public record AtualizarAutorRequestDto( [Required( ErrorMessage = "Nome é obrigatório." )] string Nome );
    #endregion

    #region Assunto
    public record CriarAssuntoRequestDto( [Required( ErrorMessage = "Descrição é obrigatório." )] string Descricao );
    public record AtualizarAssuntoRequestDto( [Required] string Descricao );
    #endregion
}
