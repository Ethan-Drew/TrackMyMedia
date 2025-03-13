using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackMyMedia.Server.Data;
using TrackMyMedia.Shared.Models;

public class ComicService : IComicService
{
    private readonly TrackMyMediaDbContext Context;

    public ComicService(TrackMyMediaDbContext context)
    {
        Context = context;
    }

    public async Task<ComicModel> CreateComicAsync(ComicModel Comic)
    {
        Comic.DateAdded= DateTime.Now;
        Context.Comics.Add(Comic);
        await Context.SaveChangesAsync();
        return Comic;
    }

    public async Task<List<ComicModel>> GetComicsAsync()
    {
        return await Context.Comics.ToListAsync();
    }

    public async Task<ComicModel> GetComicByIdAsync(int id)
    {
        return await Context.Comics
            .FirstOrDefaultAsync(m => m.MediaId == id);
    }

    public async Task<ComicModel> UpdateComicAsync(int id, ComicModel updatedComic)
    {
        var existingComic = await Context.Comics.FindAsync(id);
        if (existingComic == null)
            return null; 

        // Update fields
        existingComic.Title = updatedComic.Title;
        existingComic.Description = updatedComic.Description;
        existingComic.ReleaseDate = updatedComic.ReleaseDate;
        existingComic.MyRating = updatedComic.MyRating;

        await Context.SaveChangesAsync();
        return existingComic;
    }

    public async Task<bool> DeleteComicAsync(int id)
    {
        var Comic = await Context.Comics.FindAsync(id);
        if (Comic == null)
            return false; // Comic not found

        Context.Comics.Remove(Comic);
        await Context.SaveChangesAsync();
        return true;
    }
}
