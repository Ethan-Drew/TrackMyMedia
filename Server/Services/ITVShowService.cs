using System.Collections.Generic;
using System.Threading.Tasks;
using TrackMyMedia.Shared.Models;

public interface ITVShowService
{
    Task<TVShowModel> CreateTVShowAsync(TVShowModel TVShow);
    Task<List<TVShowModel>> GetTVShowsAsync();
    Task<TVShowModel> GetTVShowByIdAsync(int id);
    Task<TVShowModel> UpdateTVShowAsync(int id, TVShowModel updatedTVShow);
    Task<bool> DeleteTVShowAsync(int id);
}
