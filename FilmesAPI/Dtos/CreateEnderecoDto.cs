using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Dtos;

public class CreateEnderecoDto
{
    [Required(ErrorMessage = "O campo logradouro é obrigatório")]
    public string Logradouro { get; set; }
    [Required(ErrorMessage = "O campo número é obrigatório")]
    public int Numero { get; set; }
}
