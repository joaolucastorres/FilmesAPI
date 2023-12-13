using AutoMapper;
using FilmesAPI.Dtos;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles;

public class UsuarioProfile: Profile
{
    public UsuarioProfile()
    {
        CreateMap<CreateUsuarioDto, Usuario>();
    }
}
