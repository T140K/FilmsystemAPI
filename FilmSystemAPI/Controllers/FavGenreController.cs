using FilmSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

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

        [HttpPost("AddFavGenre/{personId}/{genreId}")]
        public async Task<IActionResult> AddFavGenre(int personId, int genreId)
        {
            try
            {
                var existingFavGenre = await _context.FavGenres
                    .FirstOrDefaultAsync(fg => fg.Person == personId && fg.Genre == genreId);

                if (existingFavGenre != null)
                {
                    return BadRequest("FavGenre already exists for the person and genre.");
                }

                var person = await _context.People.FindAsync(personId);
                var genre = await _context.Genre.FindAsync(genreId);

                if (person == null || genre == null)
                {
                    return BadRequest("Invalid Person or Genre");
                }

                var newFavGenre = new FavGenre
                {
                    Person = personId,
                    Genre = genreId
                };

                _context.FavGenres.Add(newFavGenre);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("MovieRecommendation/{tmdbId}")]
        public async Task<ActionResult> MovieReccomendation(int tmdbId)
        {
            var tmdbUrl = $"https://api.themoviedb.org/3/discover/movie?api_key=6483a9c164f7dc2408db3fe747bcdefd&with_genres={tmdbId}";

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(tmdbUrl);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return Ok(responseBody);
        }
    }
}
