using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CinemaController : ControllerBase
{
    private FilmeContext _context;
    private IMapper _mapper;
    public CinemaController(FilmeContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AdicionaCinema([FromBody] CreateCinemaDto form)
    {
        Cinema cinema = _mapper.Map<Cinema>(form);
        _context.Cinema.Add(cinema);
        _context.SaveChanges();
        ReadCinemaDto cinemaDto = _mapper.Map<ReadCinemaDto>(cinema);
        return CreatedAtAction(nameof(RecuperaCinemaPorId), new { id = cinema.Id }, cinemaDto);
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaCinemaPorId(int id)
    {
        Cinema? cinemaEncontrado = _context.Cinema.FirstOrDefault(c => c.Id == id);
        if (cinemaEncontrado == null) return NotFound();
        ReadCinemaDto cinemaDto = _mapper.Map<ReadCinemaDto>(cinemaEncontrado);
        return Ok(cinemaDto);
    }

    [HttpGet]
    public IEnumerable<ReadCinemaDto> RecuperaCinemas([FromQuery] int skip = 0, 
        [FromQuery] int take = 10,
        [FromQuery] int? enderecoId = null)
    {
        if(enderecoId == null)
        {
           return _mapper.Map<List<ReadCinemaDto>>(_context.Cinema.Skip(skip).Take(take).ToList());
        }
        else
        {
            return _mapper.Map<List<ReadCinemaDto>>(_context
                .Cinema
                .FromSqlRaw($"SELECT * FROM cinema WHERE cinema.EnderecoId = {enderecoId}")
                .ToList()
                );
        }
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaCinema(int id, [FromBody] UpdateCinemaDto form)
    {
        Cinema? cinema = _context.Cinema.FirstOrDefault(c => c.Id == id);
        if (cinema == null) return NotFound();
        _mapper.Map<Cinema>(form);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeletaCinema(int id)
    {
        Cinema? cinema = _context.Cinema.FirstOrDefault(c => c.Id == id);
        if (cinema == null) return NotFound();
        _context.Remove(cinema);
        _context.SaveChanges();
        return NoContent();
    }
}
