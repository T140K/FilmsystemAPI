using FilmSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly FilmSystemContext _context;

        public MovieController(FilmSystemContext context)
        {
            _context = context;
        }

        [HttpGet("GetMovieByPersonId/{personId}")]
        public async Task<ActionResult<List<Movie>>> GetMoviesById(int personId)
        {
            var movies = await _context.Movies
                .Where(m => m.Uploader == personId)
                .Select(m => m.Name)
                .ToListAsync();


            return Ok(movies);
        }
    }
}
