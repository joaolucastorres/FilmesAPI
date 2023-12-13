using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Dtos;

public class CreateSessaoDto
{
    [Required]
    public int FilmeId { get; set; }
    [Required]
    public int CinemaId { get; set; }
}
