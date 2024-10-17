using Core.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configuracoes
{
    public class LivroPrecoConfiguracao : IEntityTypeConfiguration<LivroPreco>
    {
        public void Configure( EntityTypeBuilder<LivroPreco> builder )
        {
            builder.ToTable( "Livro_Preco" );

            builder.
                HasKey( x => new { x.LivroId, x.FormaCompra } );

            builder.Property( x => x.Preco );
            builder.Property( x => x.FormaCompra );

            builder.HasOne( x => x.Livro );
        }
    }
}
