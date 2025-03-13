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
    public class ComicController : ControllerBase
    {
        private readonly IComicService ComicService;

        public ComicController(IComicService ComicService)
        {
            this.ComicService = ComicService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateComic([FromBody] ComicModel Comic)
        {
            if (Comic == null)
            {
                return BadRequest("Comic data is required.");
            }

            var createdComic = await ComicService.CreateComicAsync(Comic);
            return CreatedAtAction(nameof(GetComicById), new { id = createdComic.MediaId }, createdComic);
        }

        [HttpGet]
        public async Task<IActionResult> GetComics()
        {
            var Comics = await ComicService.GetComicsAsync();
            return Ok(Comics);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetComicById(int id)
        {
            var Comic = await ComicService.GetComicByIdAsync(id);

            if (Comic == null)
            {
                return NotFound($"Comic with ID {id} not found.");
            }

            return Ok(Comic);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComic(int id, [FromBody] ComicModel updatedComic)
        {
            var Comic = await ComicService.UpdateComicAsync(id, updatedComic);

            if (Comic == null)
            {
                return NotFound($"Comic with ID {id} not found.");
            }

            return Ok(Comic);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComic(int id)
        {
            var result = await ComicService.DeleteComicAsync(id);

            if (!result)
            {
                return NotFound($"Comic with ID {id} not found.");
            }

            return NoContent(); 
        }
    }
}
