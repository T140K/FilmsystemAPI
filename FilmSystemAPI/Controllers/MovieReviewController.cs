using FilmSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieReviewController : ControllerBase
    {
        private readonly FilmSystemContext _context;

        public MovieReviewController(FilmSystemContext context)
        {
            _context = context;
        }

        [HttpGet("GetReviewByPersonId/{personId}")]
        public async Task<ActionResult<List<MovieReview>>> GetRatingById(int personId)
        {
            var reviews = await _context.MovieReviews
                .Include(r => r.ReviewPersonNavigation) // include the related entity
                .Include(r => r.MovieNavigation) // include the related entity
                .Where(r => r.ReviewPerson == personId)
                .Select(r => new
                {
                    ReviewPersonName = r.ReviewPersonNavigation.FirstName, // select the name property
                    r.Rating,
                    r.Review,
                    MovieName = r.MovieNavigation.Name // select the title property
                })
                .ToListAsync();

            return Ok(reviews);
        }

        [HttpPost("AddMovieReview/{personId}/{movieId}/{rating}/{review}")]
        public async Task<IActionResult> AddFavGenre(int personId, int movieId, int rating, string review)
        {
            try
            {
                var existingRating = await _context.MovieReviews
                    .FirstOrDefaultAsync(fg => fg.ReviewPerson == personId && fg.Movie == movieId);

                if (existingRating != null)
                {
                    return BadRequest("You already gave a review for this movie");
                }

                var person = await _context.People.FindAsync(personId);
                var movie = await _context.Movies.FindAsync(movieId);

                if (person == null || movie == null)
                {
                    return BadRequest("Person or movie doesnt exist");
                }

                var newMovieReview = new MovieReview
                {
                    ReviewPerson = personId,
                    Movie = movieId,
                    Rating = rating,
                    Review = review
                };

                _context.MovieReviews.Add(newMovieReview);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}