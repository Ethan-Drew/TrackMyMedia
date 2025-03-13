using System.Collections.Generic;
using System.Threading.Tasks;
using TrackMyMedia.Shared.Models;

public interface IVideoGameService
{
    Task<VideoGameModel> CreateVideoGameAsync(VideoGameModel VideoGame);
    Task<List<VideoGameModel>> GetVideoGamesAsync();
    Task<VideoGameModel> GetVideoGameByIdAsync(int id);
    Task<VideoGameModel> UpdateVideoGameAsync(int id, VideoGameModel updatedVideoGame);
    Task<bool> DeleteVideoGameAsync(int id);
}
