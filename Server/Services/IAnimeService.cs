using System.Collections.Generic;
using System.Threading.Tasks;
using TrackMyMedia.Shared.Models;

public interface IAnimeService
{
    Task<AnimeModel> CreateAnimeAsync(AnimeModel Anime);
    Task<List<AnimeModel>> GetAnimesAsync();
    Task<AnimeModel> GetAnimeByIdAsync(int id);
    Task<AnimeModel> UpdateAnimeAsync(int id, AnimeModel updatedAnime);
    Task<bool> DeleteAnimeAsync(int id);
}
