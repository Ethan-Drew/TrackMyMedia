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
    public class TVShowController : ControllerBase
    {
        private readonly ITVShowService TVShowService;

        public TVShowController(ITVShowService TVShowService)
        {
            this.TVShowService = TVShowService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTVShow([FromBody] TVShowModel TVShow)
        {
            if (TVShow == null)
            {
                return BadRequest("TVShow data is required.");
            }

            var createdTVShow = await TVShowService.CreateTVShowAsync(TVShow);
            return CreatedAtAction(nameof(GetTVShowById), new { id = createdTVShow.MediaId }, createdTVShow);
        }

        [HttpGet]
        public async Task<IActionResult> GetTVShows()
        {
            var TVShows = await TVShowService.GetTVShowsAsync();
            return Ok(TVShows);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTVShowById(int id)
        {
            var TVShow = await TVShowService.GetTVShowByIdAsync(id);

            if (TVShow == null)
            {
                return NotFound($"TVShow with ID {id} not found.");
            }

            return Ok(TVShow);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTVShow(int id, [FromBody] TVShowModel updatedTVShow)
        {
            var TVShow = await TVShowService.UpdateTVShowAsync(id, updatedTVShow);

            if (TVShow == null)
            {
                return NotFound($"TVShow with ID {id} not found.");
            }

            return Ok(TVShow);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTVShow(int id)
        {
            var result = await TVShowService.DeleteTVShowAsync(id);

            if (!result)
            {
                return NotFound($"TVShow with ID {id} not found.");
            }

            return NoContent(); 
        }
    }
}
