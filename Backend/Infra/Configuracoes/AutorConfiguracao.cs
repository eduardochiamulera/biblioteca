using Core.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configuracoes
{
    public class AutorConfiguracao : IEntityTypeConfiguration<Autor>
    {
        public void Configure( EntityTypeBuilder<Autor> builder )
        {
            builder.ToTable( "Autor" );

            builder.HasKey( x => x.Id );

            builder.Property( x => x.Id ).HasColumnName( "Cod" );

            builder.Property( x => x.Nome );
        }
    }
}
