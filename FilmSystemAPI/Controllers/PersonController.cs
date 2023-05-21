using FilmSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly FilmSystemContext _context;

        public PersonController(FilmSystemContext context)
        {
            _context = context;
        }

        [HttpGet("GetPeople")]
        public async Task<ActionResult<List<FavGenre>>> GetAllPeople()
        {
            return Ok(await _context.People.ToListAsync());
        }

        [HttpGet("GetPersonById/{personId}")]
        public async Task<ActionResult<List<FavGenre>>> GetPersonById(int personId)
        {
            var person = await _context.People
                .Where(p => p.Id == personId)
                .Select(p => new
                {
                    p.Id, p.FirstName, p.LastName, p.Email
                })
                .ToListAsync();

            return Ok(person);
        }
    }
}
