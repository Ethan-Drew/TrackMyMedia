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
    public class VideoGameController : ControllerBase
    {
        private readonly IVideoGameService VideoGameService;

        public VideoGameController(IVideoGameService VideoGameService)
        {
            this.VideoGameService = VideoGameService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateVideoGame([FromBody] VideoGameModel VideoGame)
        {
            if (VideoGame == null)
            {
                return BadRequest("VideoGame data is required.");
            }

            var createdVideoGame = await VideoGameService.CreateVideoGameAsync(VideoGame);
            return CreatedAtAction(nameof(GetVideoGameById), new { id = createdVideoGame.MediaId }, createdVideoGame);
        }

        [HttpGet]
        public async Task<IActionResult> GetVideoGames()
        {
            var VideoGames = await VideoGameService.GetVideoGamesAsync();
            return Ok(VideoGames);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVideoGameById(int id)
        {
            var VideoGame = await VideoGameService.GetVideoGameByIdAsync(id);

            if (VideoGame == null)
            {
                return NotFound($"VideoGame with ID {id} not found.");
            }

            return Ok(VideoGame);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVideoGame(int id, [FromBody] VideoGameModel updatedVideoGame)
        {
            var VideoGame = await VideoGameService.UpdateVideoGameAsync(id, updatedVideoGame);

            if (VideoGame == null)
            {
                return NotFound($"VideoGame with ID {id} not found.");
            }

            return Ok(VideoGame);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVideoGame(int id)
        {
            var result = await VideoGameService.DeleteVideoGameAsync(id);

            if (!result)
            {
                return NotFound($"VideoGame with ID {id} not found.");
            }

            return NoContent(); 
        }
    }
}
