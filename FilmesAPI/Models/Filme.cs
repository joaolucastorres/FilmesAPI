using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models;

public class Filme
{
    [Required]
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "O título do filme é obrigatório")]
    [MaxLength(50, ErrorMessage = "O título deve ter no máximo 50 caracteres")]
    public string Titulo { get; set; }
    [Required(ErrorMessage = "O gênero do filme é obrigatório")]
    [MaxLength(50, ErrorMessage = "O gênero deve ter no máximo 50 caracteres")]
    public string Genero { get; set; }
    [Required(ErrorMessage = "A duração do filme é obrigatório")]
    [Range(70, 600, ErrorMessage = "A duração deve ter entre 70 e 600 minutos")]
    public int Duracao { get; set; }

    public virtual ICollection<Sessao> Sessoes { get; set; }
 }
