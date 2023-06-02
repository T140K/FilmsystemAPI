﻿using FilmSystemAPI.Models;
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

        [HttpGet("GetAllMovies")]
        public async Task<ActionResult<List<Movie>>> GetMovieByPersonId()
        {
            var movies = await _context.Movies
                .Select(m => new
                {
                    m.Id,
                    m.Name,
                    m.MovieGenre,
                    m.Link,
                    m.Uploader
                })
                .ToListAsync();


            return Ok(movies);
        }

        [HttpGet("GetMovieByPersonId/{personId}")]
        public async Task<ActionResult<List<Movie>>> GetMovieByPersonId(int personId)
        {
            var movies = await _context.Movies
                .Where(m => m.Uploader == personId)
                .Select(m => new 
                { 

                    m.Name, m.MovieGenre, m.Link
                })
                .ToListAsync();


            return Ok(movies);
        }

        [HttpPost("AddNewMovie/{name}/{link}/{movieGenre}/{uploader}")]
        public async Task<IActionResult> AddNewMovie(string name, string link, int movieGenre, int uploader)
        {
            try
            {
                var existingMovies = await _context.Movies
                    .FirstOrDefaultAsync(m => m.Link == link && m.Name == name);

                if (existingMovies != null)
                {
                    return BadRequest("This is already uploaded.");
                }

                var cMovieGenre = await _context.Genre.FindAsync(movieGenre);
                var cUploader = await _context.People.FindAsync(uploader);

                if (cMovieGenre == null || cUploader == null)
                {
                    return BadRequest("Invalid uploader or genre");
                }

                var newMovie = new Movie
                {
                    Uploader = uploader,
                    Link = link,
                    MovieGenre = movieGenre,
                    Name = name
                };

                _context.Movies.Add(newMovie);
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
