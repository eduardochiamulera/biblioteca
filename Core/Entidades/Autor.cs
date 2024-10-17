namespace Core.Entidades
{
    public class Autor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public ICollection<Livro> Livros { get; set; } = new List<Livro>();
    }
}
