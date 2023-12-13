using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Dtos;

public class CreateCinemaDto
{
    [Required(ErrorMessage = "O campo nome é obrigatório")]
    public string Nome { get; set; }
    [Required(ErrorMessage = "O campo EnderecoId é obrigatório")]
    public int EnderecoId { get; set; }
}
