using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackMyMedia.Server.Data;
using TrackMyMedia.Shared.Models;

public class AnimeService : IAnimeService
{
    private readonly TrackMyMediaDbContext Context;

    public AnimeService(TrackMyMediaDbContext context)
    {
        Context = context;
    }

    public async Task<AnimeModel> CreateAnimeAsync(AnimeModel Anime)
    {
        Anime.DateAdded= DateTime.Now;
        Context.Anime.Add(Anime);
        await Context.SaveChangesAsync();
        return Anime;
    }

    public async Task<List<AnimeModel>> GetAnimesAsync()
    {
        return await Context.Anime.ToListAsync();
    }

    public async Task<AnimeModel> GetAnimeByIdAsync(int id)
    {
        return await Context.Anime
            .FirstOrDefaultAsync(m => m.MediaId == id);
    }

    public async Task<AnimeModel> UpdateAnimeAsync(int id, AnimeModel updatedAnime)
    {
        var existingAnime = await Context.Anime.FindAsync(id);
        if (existingAnime == null)
            return null; 

        // Update fields
        existingAnime.Title = updatedAnime.Title;
        existingAnime.Description = updatedAnime.Description;
        existingAnime.ReleaseDate = updatedAnime.ReleaseDate;
        existingAnime.MyRating = updatedAnime.MyRating;

        await Context.SaveChangesAsync();
        return existingAnime;
    }

    public async Task<bool> DeleteAnimeAsync(int id)
    {
        var Anime = await Context.Anime.FindAsync(id);
        if (Anime == null)
            return false; // Anime not found

        Context.Anime.Remove(Anime);
        await Context.SaveChangesAsync();
        return true;
    }
}
