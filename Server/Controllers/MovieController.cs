using Microsoft.AspNetCore.Mvc;
using TrackMyMedia.Server.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackMyMedia.Shared.Models;
using Microsoft.AspNetCore.Authorization;

namespace TrackMyMedia.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService movieService;

        public MovieController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovie([FromBody] MovieModel movie)
        {
            if (movie == null)
            {
                return BadRequest("Movie data is required.");
            }

            var createdMovie = await movieService.CreateMovieAsync(movie);
            return CreatedAtAction(nameof(GetMovieById), new { id = createdMovie.MediaId }, createdMovie);
        }

        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            var movies = await movieService.GetMoviesAsync();
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            var movie = await movieService.GetMovieByIdAsync(id);

            if (movie == null)
            {
                return NotFound($"Movie with ID {id} not found.");
            }

            return Ok(movie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, [FromBody] MovieModel updatedMovie)
        {
            var movie = await movieService.UpdateMovieAsync(id, updatedMovie);

            if (movie == null)
            {
                return NotFound($"Movie with ID {id} not found.");
            }

            return Ok(movie);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var result = await movieService.DeleteMovieAsync(id);

            if (!result)
            {
                return NotFound($"Movie with ID {id} not found.");
            }

            return NoContent(); 
        }
    }
}
