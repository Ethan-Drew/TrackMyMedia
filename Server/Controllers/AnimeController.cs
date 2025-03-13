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
    public class AnimeController : ControllerBase
    {
        private readonly IAnimeService AnimeService;

        public AnimeController(IAnimeService AnimeService)
        {
            this.AnimeService = AnimeService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnime([FromBody] AnimeModel Anime)
        {
            if (Anime == null)
            {
                return BadRequest("Anime data is required.");
            }

            var createdAnime = await AnimeService.CreateAnimeAsync(Anime);
            return CreatedAtAction(nameof(GetAnimeById), new { id = createdAnime.MediaId }, createdAnime);
        }

        [HttpGet]
        public async Task<IActionResult> GetAnimes()
        {
            var Animes = await AnimeService.GetAnimesAsync();
            return Ok(Animes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnimeById(int id)
        {
            var Anime = await AnimeService.GetAnimeByIdAsync(id);

            if (Anime == null)
            {
                return NotFound($"Anime with ID {id} not found.");
            }

            return Ok(Anime);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnime(int id, [FromBody] AnimeModel updatedAnime)
        {
            var Anime = await AnimeService.UpdateAnimeAsync(id, updatedAnime);

            if (Anime == null)
            {
                return NotFound($"Anime with ID {id} not found.");
            }

            return Ok(Anime);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnime(int id)
        {
            var result = await AnimeService.DeleteAnimeAsync(id);

            if (!result)
            {
                return NotFound($"Anime with ID {id} not found.");
            }

            return NoContent(); 
        }
    }
}
