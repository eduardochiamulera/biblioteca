using Core.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configuracoes
{
    public class LivroConfiguracao : IEntityTypeConfiguration<Livro>
    {
        public void Configure( EntityTypeBuilder<Livro> builder )
        {
            builder.ToTable( "Livro" );

            builder.
                HasKey( x => x.Codigo );
            builder.Property( x => x.Titulo );
            builder.Property( x => x.AnoPublicacao );
            builder.Property( x => x.Edicao );
            builder.Property( x => x.Editora );

            builder.Property( x => x.Codigo ).HasColumnName( "CodL" );

            builder.HasMany( x => x.Autores ).WithMany( x => x.Livros )
                .UsingEntity( "Livro_Autor",
                l => l.HasOne( typeof( Autor ) ).WithMany().HasForeignKey( "AutorId" ).HasPrincipalKey( nameof( Autor.Id ) ),
                r => r.HasOne( typeof( Livro ) ).WithMany().HasForeignKey( "LivroId" ).HasPrincipalKey( nameof( Livro.Codigo ) ),
                j =>
                {
                    j.HasKey( "LivroId", "AutorId" );
                    j.Property<int>( "AutorId" ).HasColumnName( "Autor_CodAu" );
                    j.Property<int>( "LivroId" ).HasColumnName( "Livro_CodL" );
                } );

            builder.HasMany( x => x.Assuntos ).WithMany()
                .UsingEntity( "Livro_Assunto",
                l => l.HasOne( typeof( Assunto ) ).WithMany().HasForeignKey( "AssuntoId" ).HasPrincipalKey( nameof( Assunto.Id ) ),
                r => r.HasOne( typeof( Livro ) ).WithMany().HasForeignKey( "LivroId" ).HasPrincipalKey( nameof( Livro.Codigo ) ),
                j =>
                {
                    j.HasKey( "LivroId", "AssuntoId" );
                    j.Property<int>( "AssuntoId" ).HasColumnName( "Assunto_CodAs" );
                    j.Property<int>( "LivroId" ).HasColumnName( "Livro_CodL" );

                } );


            builder.HasMany( x => x.Precos ).WithOne( x => x.Livro );
        }
    }
}
