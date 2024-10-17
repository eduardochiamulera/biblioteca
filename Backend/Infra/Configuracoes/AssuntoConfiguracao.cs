using Core.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configuracoes
{
    public class AssuntoConfiguracao : IEntityTypeConfiguration<Assunto>
    {
        public void Configure( EntityTypeBuilder<Assunto> builder )
        {
            builder.ToTable( "Assunto" );
            builder.HasKey( x => x.Id );

            builder.Property( x => x.Id ).HasColumnName( "CodAs" );

            builder.Property( x => x.Descricao );
        }
    }
}
