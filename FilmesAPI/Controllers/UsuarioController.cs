using AutoMapper;
using FilmesAPI.Dtos;
using FilmesAPI.Models;
using FilmesAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UsuarioController: ControllerBase
{
    private UsuarioService _service;
    public UsuarioController(UsuarioService service)
    {
       _service = service;
    }

    [HttpPost("signin")]
    public async Task<IActionResult> CadastraUsuario([FromBody] CreateUsuarioDto form)
    {
        
        await _service.CadastraUsuario(form);
        return Ok("Usuário cadastrado com sucesso!");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUsuarioDto form)
    {
        string token = await _service.Login(form);
        return Ok(token);
    }
}
