using FilmSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly FilmSystemContext _context;

        public GenresController(FilmSystemContext context)
        {
            _context = context;
        }

        [HttpGet("GetGenres")]
        public async Task<ActionResult<List<Genres>>> GetGenresByPersonId()
        {
            var genres = await _context.Genre
                .Select(g => new
                {
                    g.Id,
                    g.Name,
                    g.Description
                })
                .ToListAsync();

            return Ok(genres);
        }
    }
}
