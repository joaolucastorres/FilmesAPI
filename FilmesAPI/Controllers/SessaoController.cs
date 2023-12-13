using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class SessaoController: ControllerBase
{
    private FilmeContext _context;
    private IMapper _mapper;
    public SessaoController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AdicionaSessao([FromBody] CreateSessaoDto form)
    {
        Sessao sessao = _mapper.Map<Sessao>(form);
        _context.Sessao.Add(sessao);
        _context.SaveChanges();
        ReadSessaoDto sessaoDto = _mapper.Map<ReadSessaoDto>(sessao);
        return CreatedAtAction(nameof(RecuperaSessaoPorId), new { filmeId = sessao.FilmeId, cinemaId = sessao.CinemaId }, sessaoDto);
    }

    [HttpGet("{filmeId}/{cinemaId}")]
    public IActionResult RecuperaSessaoPorId(int filmeId, int cinemaId)
    {
        Sessao? sessaoEncontrada = _context.Sessao.FirstOrDefault(s => s.FilmeId == filmeId && s.CinemaId == cinemaId);
        if (sessaoEncontrada == null) return NotFound();
        ReadSessaoDto sessaoDto = _mapper.Map<ReadSessaoDto>(sessaoEncontrada);
        return Ok(sessaoDto);
    }

    [HttpGet]
    public IEnumerable<ReadSessaoDto> RecuperaSessoes([FromQuery] int skip = 0, [FromQuery] int take = 10)
    {
        List<Sessao> lista = _context.Sessao.ToList();
        return _mapper.Map<List<ReadSessaoDto>>(_context.Sessao.Skip(skip).Take(take).ToList());
    }

    [HttpPut("{filmeId}/{cinemaId}")]
    public IActionResult AtualizaSessao(int filmeId, int cinemaId, [FromBody] UpdateSessaoDto form)
    {
        Sessao? sessao = _context.Sessao.FirstOrDefault(s => s.FilmeId == filmeId && s.CinemaId == cinemaId);
        if (sessao == null) return NotFound();
        _mapper.Map<Sessao>(form);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{filmeId}/{cinemaId}")]
    public IActionResult DeletaSessao(int filmeId, int cinemaId)
    {
        Sessao? sessao = _context.Sessao.FirstOrDefault(s => s.FilmeId == filmeId && s.CinemaId == cinemaId);
        if (sessao == null) return NotFound();
        _context.Remove(sessao);
        _context.SaveChanges();
        return NoContent();
    }
}
