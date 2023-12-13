using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("{controller}")]
public class EnderecoController: ControllerBase
{
    private FilmeContext _context;
    private IMapper _mapper;
    public EnderecoController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AdicionaEndereco([FromBody] CreateEnderecoDto form)
    {
        Endereco endereco = _mapper.Map<Endereco>(form);
        _context.Endereco.Add(endereco);
        _context.SaveChanges();
        ReadEnderecoDto enderecoDto = _mapper.Map<ReadEnderecoDto>(endereco);
        return CreatedAtAction(nameof(RecuperaEnderecoPorId), new { id = endereco.Id }, enderecoDto);
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaEnderecoPorId(int id)
    {
        Endereco? enderecoEncontrado = _context.Endereco.FirstOrDefault(c => c.Id == id);
        if (enderecoEncontrado == null) return NotFound();
        ReadEnderecoDto enderecoDto = _mapper.Map<ReadEnderecoDto>(enderecoEncontrado);
        return Ok(enderecoDto);
    }

    [HttpGet]
    public IEnumerable<ReadEnderecoDto> RecuperaEnderecos([FromQuery] int skip = 0, [FromQuery] int take = 10)
    {
        return _mapper.Map<List<ReadEnderecoDto>>(_context.Endereco.Skip(skip).Take(take).ToList());
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaEndereco(int id, [FromBody] UpdateEnderecoDto form)
    {
        Endereco? endereco = _context.Endereco.FirstOrDefault(c => c.Id == id);
        if (endereco == null) return NotFound();
        _mapper.Map<Endereco>(form);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeletaEndereco(int id)
    {
        Endereco? endereco = _context.Endereco.FirstOrDefault(c => c.Id == id);
        if (endereco == null) return NotFound();
        _context.Remove(endereco);
        _context.SaveChanges();
        return NoContent();
    }
}
