using System.Collections.Generic;
using System.Threading.Tasks;
using TrackMyMedia.Shared.Models;

public interface IComicService
{
    Task<ComicModel> CreateComicAsync(ComicModel Comic);
    Task<List<ComicModel>> GetComicsAsync();
    Task<ComicModel> GetComicByIdAsync(int id);
    Task<ComicModel> UpdateComicAsync(int id, ComicModel updatedComic);
    Task<bool> DeleteComicAsync(int id);
}
