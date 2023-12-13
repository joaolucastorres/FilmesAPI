using FilmesAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Data;

public class FilmeContext: IdentityDbContext<Usuario>
{
    public FilmeContext(DbContextOptions<FilmeContext> opts): base(opts)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //Definindo id composto para a entidade Sessao
        modelBuilder.Entity<Sessao>()
             .HasKey(sessao => new
             {
                 sessao.FilmeId,
                 sessao.CinemaId
             });

        //Definindo relação de um para muitos entre Sessao e Cinema
        modelBuilder.Entity<Sessao>()
            .HasOne(sessao => sessao.Cinema)
            .WithMany(cinema => cinema.Sessoes)
            .HasForeignKey(sessao => sessao.CinemaId);

        //Definindo relação de um para muitos entre Sessao e Filme
        //Logo temos uma relação de muitos para muitos entre Filme e Cinema, onde Sessao é a tabela de relacionamento
        modelBuilder.Entity<Sessao>()
            .HasOne(sessao => sessao.Filme)
            .WithMany(filme => filme.Sessoes)
            .HasForeignKey(sessao => sessao.FilmeId);

        //Definindo exclusão de endereço de modo cascata para modo restrito
        modelBuilder.Entity<Endereco>()
            .HasOne(endereco => endereco.Cinema)
            .WithOne(cinema => cinema.Endereco)
            .OnDelete(DeleteBehavior.Restrict);
    }

    public DbSet<Filme> Filmes { get; set; }
    public DbSet<Cinema> Cinema { get; set; }
    public DbSet<Endereco> Endereco { get; set; }
    public DbSet<Sessao> Sessao { get; set; }
}
