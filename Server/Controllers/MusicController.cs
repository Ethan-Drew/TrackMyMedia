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
    public class MusicController : ControllerBase
    {
        private readonly IMusicService MusicService;

        public MusicController(IMusicService MusicService)
        {
            this.MusicService = MusicService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMusic([FromBody] MusicModel Music)
        {
            if (Music == null)
            {
                return BadRequest("Music data is required.");
            }

            var createdMusic = await MusicService.CreateMusicAsync(Music);
            return CreatedAtAction(nameof(GetMusicById), new { id = createdMusic.MediaId }, createdMusic);
        }

        [HttpGet]
        public async Task<IActionResult> GetMusics()
        {
            var Musics = await MusicService.GetMusicsAsync();
            return Ok(Musics);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMusicById(int id)
        {
            var Music = await MusicService.GetMusicByIdAsync(id);

            if (Music == null)
            {
                return NotFound($"Music with ID {id} not found.");
            }

            return Ok(Music);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMusic(int id, [FromBody] MusicModel updatedMusic)
        {
            var Music = await MusicService.UpdateMusicAsync(id, updatedMusic);

            if (Music == null)
            {
                return NotFound($"Music with ID {id} not found.");
            }

            return Ok(Music);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMusic(int id)
        {
            var result = await MusicService.DeleteMusicAsync(id);

            if (!result)
            {
                return NotFound($"Music with ID {id} not found.");
            }

            return NoContent(); 
        }
    }
}
