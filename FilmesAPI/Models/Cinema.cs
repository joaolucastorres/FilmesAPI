using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models;

public class Cinema
{
    [Required]
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "O campo nome é obrigatório")]
    public string Nome { get; set; }
    [Required(ErrorMessage = "O campo EnderecoId é obrigatório")]
    public int EnderecoId { get; set; }
    public virtual Endereco Endereco { get; set; }

    public virtual ICollection<Sessao> Sessoes { get; set; }
}
