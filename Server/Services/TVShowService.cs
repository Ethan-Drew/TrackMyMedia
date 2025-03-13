using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackMyMedia.Server.Data;
using TrackMyMedia.Shared.Models;

public class TVShowService : ITVShowService
{
    private readonly TrackMyMediaDbContext Context;

    public TVShowService(TrackMyMediaDbContext context)
    {
        Context = context;
    }

    public async Task<TVShowModel> CreateTVShowAsync(TVShowModel TVShow)
    {
        TVShow.DateAdded= DateTime.Now;
        Context.TVShows.Add(TVShow);
        await Context.SaveChangesAsync();
        return TVShow;
    }

    public async Task<List<TVShowModel>> GetTVShowsAsync()
    {
        return await Context.TVShows.ToListAsync();
    }

    public async Task<TVShowModel> GetTVShowByIdAsync(int id)
    {
        return await Context.TVShows
            .FirstOrDefaultAsync(m => m.MediaId == id);
    }

    public async Task<TVShowModel> UpdateTVShowAsync(int id, TVShowModel updatedTVShow)
    {
        var existingTVShow = await Context.TVShows.FindAsync(id);
        if (existingTVShow == null)
            return null; 

        // Update fields
        existingTVShow.Title = updatedTVShow.Title;
        existingTVShow.Description = updatedTVShow.Description;
        existingTVShow.ReleaseDate = updatedTVShow.ReleaseDate;
        existingTVShow.MyRating = updatedTVShow.MyRating;

        await Context.SaveChangesAsync();
        return existingTVShow;
    }

    public async Task<bool> DeleteTVShowAsync(int id)
    {
        var TVShow = await Context.TVShows.FindAsync(id);
        if (TVShow == null)
            return false; // TVShow not found

        Context.TVShows.Remove(TVShow);
        await Context.SaveChangesAsync();
        return true;
    }
}
