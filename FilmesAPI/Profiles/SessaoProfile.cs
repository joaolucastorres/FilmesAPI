using AutoMapper;
using FilmesAPI.Dtos;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles;

public class SessaoProfile: Profile
{
    public SessaoProfile()
    {
        CreateMap<CreateSessaoDto, Sessao>();
        CreateMap<UpdateSessaoDto, Sessao>();
        CreateMap<Sessao, ReadSessaoDto>();
    }
}
