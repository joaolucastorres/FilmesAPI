using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmeController: ControllerBase
{
    private FilmeContext _context;
    private IMapper _mapper;

    public FilmeController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AdicionarFilme([FromBody]CreateFilmeDto form)
    {
        Filme filme = _mapper.Map<Filme>(form);
        _context.Filmes.Add(filme);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperarFilmePorId), 
            new {id = filme.Id}, 
            filme);
    }

    [Authorize(Policy= "IdadeMinima")]
    [HttpGet]
    public IEnumerable<ReadFilmeDto> RecuperarFilmes([FromQuery] int skip = 0, 
        [FromQuery]int take= 10,
        [FromQuery] string? nomeCinema = null)
    {
        if (nomeCinema == null)
        {
            return _mapper.Map<List<ReadFilmeDto>>(_context.Filmes
                .Skip(skip)
                .Take(take)
                .ToList());

        }
        else
        {
            return _mapper.Map<List<ReadFilmeDto>>(_context.Filmes
                .Skip(skip)
                .Take(take)
                .Where(filme => filme.Sessoes
                    .Any(sessao => sessao.Cinema.Nome == nomeCinema))
                .ToList());

        }
    }

    [HttpGet("{id}")]
    public IActionResult RecuperarFilmePorId(int id)
    {
        Filme? filme = _context.Filmes.FirstOrDefault(f => f.Id == id);
        if (filme == null) return NotFound();
        ReadFilmeDto filmeDto = _mapper.Map<ReadFilmeDto>(filme);
        return Ok(filmeDto);
    }

    [HttpPut("{id}")]
    public IActionResult AtualizarFilme(int id,
        [FromBody] UpdateFilmeDto form)
    {
        Filme? filme = _context.Filmes.FirstOrDefault(f => f.Id == id);
        if (filme == null) return NotFound();
        _mapper.Map(form, filme);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpPatch("{id}")]
    public IActionResult AtualizarFilmeParcial(int id,
        JsonPatchDocument<UpdateFilmeDto> patch)
    {
        Filme? filme = _context.Filmes.FirstOrDefault(f => f.Id == id);
        if (filme == null) return NotFound();

        var filmeParaAtualizar = _mapper.Map<UpdateFilmeDto>(filme);

        patch.ApplyTo(filmeParaAtualizar, ModelState);

        if (!TryValidateModel(filmeParaAtualizar))
        {
            return ValidationProblem();
        }

        _mapper.Map(filmeParaAtualizar, filme);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeletarFilme(int id)
    {
        Filme? filme = _context.Filmes.FirstOrDefault(f => f.Id == id);
        if (filme == null) return NotFound();
        _context.Remove(filme);
        _context.SaveChanges();
        return NoContent();
    }

}
