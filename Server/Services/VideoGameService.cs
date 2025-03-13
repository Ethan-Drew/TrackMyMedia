using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackMyMedia.Server.Data;
using TrackMyMedia.Shared.Models;

public class VideoGameService : IVideoGameService
{
    private readonly TrackMyMediaDbContext Context;

    public VideoGameService(TrackMyMediaDbContext context)
    {
        Context = context;
    }

    public async Task<VideoGameModel> CreateVideoGameAsync(VideoGameModel VideoGame)
    {
        VideoGame.DateAdded= DateTime.Now;
        Context.VideoGames.Add(VideoGame);
        await Context.SaveChangesAsync();
        return VideoGame;
    }

    public async Task<List<VideoGameModel>> GetVideoGamesAsync()
    {
        return await Context.VideoGames.ToListAsync();
    }

    public async Task<VideoGameModel> GetVideoGameByIdAsync(int id)
    {
        return await Context.VideoGames
            .FirstOrDefaultAsync(m => m.MediaId == id);
    }

    public async Task<VideoGameModel> UpdateVideoGameAsync(int id, VideoGameModel updatedVideoGame)
    {
        var existingVideoGame = await Context.VideoGames.FindAsync(id);
        if (existingVideoGame == null)
            return null; 

        // Update fields
        existingVideoGame.Title = updatedVideoGame.Title;
        existingVideoGame.Description = updatedVideoGame.Description;
        existingVideoGame.ReleaseDate = updatedVideoGame.ReleaseDate;
        existingVideoGame.MyRating = updatedVideoGame.MyRating;

        await Context.SaveChangesAsync();
        return existingVideoGame;
    }

    public async Task<bool> DeleteVideoGameAsync(int id)
    {
        var VideoGame = await Context.VideoGames.FindAsync(id);
        if (VideoGame == null)
            return false; // VideoGame not found

        Context.VideoGames.Remove(VideoGame);
        await Context.SaveChangesAsync();
        return true;
    }
}
