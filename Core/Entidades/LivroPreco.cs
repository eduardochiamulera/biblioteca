using Core.Enumeradores;

namespace Core.Entidades
{
    public class LivroPreco
    {
        public int LivroId { get; set; }
        public Livro Livro { get; set; }
        public decimal Preco { get; set; }
        public FormaCompra FormaCompra { get; set; }
    }
}
