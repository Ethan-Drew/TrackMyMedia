using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackMyMedia.Server.Data;
using TrackMyMedia.Shared.Models;

public class MusicService : IMusicService
{
    private readonly TrackMyMediaDbContext Context;

    public MusicService(TrackMyMediaDbContext context)
    {
        Context = context;
    }

    public async Task<MusicModel> CreateMusicAsync(MusicModel Music)
    {
        Music.DateAdded= DateTime.Now;
        Context.Music.Add(Music);
        await Context.SaveChangesAsync();
        return Music;
    }

    public async Task<List<MusicModel>> GetMusicsAsync()
    {
        return await Context.Music.ToListAsync();
    }

    public async Task<MusicModel> GetMusicByIdAsync(int id)
    {
        return await Context.Music
            .FirstOrDefaultAsync(m => m.MediaId == id);
    }

    public async Task<MusicModel> UpdateMusicAsync(int id, MusicModel updatedMusic)
    {
        var existingMusic = await Context.Music.FindAsync(id);
        if (existingMusic == null)
            return null; 

        // Update fields
        existingMusic.Title = updatedMusic.Title;
        existingMusic.Description = updatedMusic.Description;
        existingMusic.ReleaseDate = updatedMusic.ReleaseDate;
        existingMusic.MyRating = updatedMusic.MyRating;

        await Context.SaveChangesAsync();
        return existingMusic;
    }

    public async Task<bool> DeleteMusicAsync(int id)
    {
        var Music = await Context.Music.FindAsync(id);
        if (Music == null)
            return false; // Music not found

        Context.Music.Remove(Music);
        await Context.SaveChangesAsync();
        return true;
    }
}
