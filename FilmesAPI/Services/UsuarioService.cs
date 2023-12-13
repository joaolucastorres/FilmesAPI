using AutoMapper;
using FilmesAPI.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace FilmesAPI.Services;

public class UsuarioService
{
    private IMapper _mapper;
    private UserManager<Usuario> _userManager;
    private SignInManager<Usuario> _signInManager;
    private TokenService _tokenService;

    public UsuarioService
        (
        IMapper mapper, 
        UserManager<Usuario> userManager, 
        SignInManager<Usuario> signInManager,
        TokenService tokenService
        )
    {
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public async Task CadastraUsuario(CreateUsuarioDto form)
    {
        Usuario usuario = _mapper.Map<Usuario>(form);

        IdentityResult resultado = await _userManager.CreateAsync(usuario, form.Password);

        if (!resultado.Succeeded)
        {
            throw new ApplicationException("Falha ao cadastrar o usuário!");
        }
    }

    public async Task<string> Login(LoginUsuarioDto form)
    {
        SignInResult resultado = await _signInManager.PasswordSignInAsync(form.Username, form.Password, false, false);

        if (!resultado.Succeeded)
        {
            throw new ApplicationException("Nome de usuário ou senha incorreto!");
        }

        Usuario usuario = _signInManager
            .UserManager
            .Users
            .FirstOrDefault(u => u.NormalizedUserName == form.Username.ToUpper())!;

        return _tokenService.GenerateToken(usuario);

    }
}
