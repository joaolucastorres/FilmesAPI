using AutoMapper;
using FilmesAPI.Dtos;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles;

public class EnderecoProfile: Profile
{
    public EnderecoProfile() { 
    CreateMap<Endereco, ReadEnderecoDto>();
    CreateMap<UpdateEnderecoDto, Endereco>();
    CreateMap<CreateEnderecoDto, Endereco>();
    }
}
