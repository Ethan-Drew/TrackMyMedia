using System.Collections.Generic;
using System.Threading.Tasks;
using TrackMyMedia.Shared.Models;

public interface IMusicService
{
    Task<MusicModel> CreateMusicAsync(MusicModel Music);
    Task<List<MusicModel>> GetMusicsAsync();
    Task<MusicModel> GetMusicByIdAsync(int id);
    Task<MusicModel> UpdateMusicAsync(int id, MusicModel updatedMusic);
    Task<bool> DeleteMusicAsync(int id);
}
