using Core.Entidades;
using Infra.Configuracoes;
using Microsoft.EntityFrameworkCore;

namespace Infra
{
	public class BibliotecaContexto : DbContext
    {
        public BibliotecaContexto( DbContextOptions<BibliotecaContexto> options ) : base( options )
        {
        }

        public DbSet<Livro> Livros { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Assunto> Assuntos { get; set; }

        protected override void OnModelCreating( ModelBuilder builder )
        {
            base.OnModelCreating( builder );
			builder.ApplyConfigurationsFromAssembly( typeof( LivroConfiguracao ).Assembly );
        }
    }
}
