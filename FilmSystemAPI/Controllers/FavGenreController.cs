using FilmSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavGenreController : ControllerBase
    {
        private readonly FilmSystemContext _context;

        public FavGenreController(FilmSystemContext context)
        {
            _context = context;
        }
        /*
        [HttpGet("GetFavGenres")]
        public async Task<ActionResult<List<FavGenre>>> GetFavGenre(int personId)
        {
            var favGenres = await _context.FavGenres
                .Include(fg => fg.PersonNavigation)
                .Include(fg => fg.GenreNavigation)
                .Where()
                .ToListAsync();

            var result = favGenres.Select(fg => new {
                Id = fg.Id,
                Person = new
                {
                    Id = fg.PersonNavigation.Id,
                    FirstName = fg.PersonNavigation.FirstName,
                    LastName = fg.PersonNavigation.LastName,
                    Email = fg.PersonNavigation.Email
                },
                Genre = new
                {
                    Id = fg.GenreNavigation.Id,
                    Name = fg.GenreNavigation.Name,
                    Description = fg.GenreNavigation.Description
                }
            }).ToList<object>();

            return Ok(result);
        }*/

        [HttpGet("GetGenresByPersonId/{personId}")]
        public async Task<ActionResult<List<FavGenre>>> GetGenresByPersonId(int personId)
        {
            var genres = await _context.FavGenres
                .Where(fg => fg.Person == personId)
                .Select(fg => fg.GenreNavigation)
                .ToListAsync();

            return Ok(genres);
        }

        /*
        [HttpGet]
        public async Task<ActionResult<List<Person>>> GetPerson()
        {
            return Ok(await _context.People.ToListAsync());
        }*/
    }
}
