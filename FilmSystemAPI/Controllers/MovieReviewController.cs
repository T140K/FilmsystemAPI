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
    }
}