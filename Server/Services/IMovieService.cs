using System.Collections.Generic;
using System.Threading.Tasks;
using TrackMyMedia.Shared.Models;

public interface IMovieService
{
    Task<MovieModel> CreateMovieAsync(MovieModel movie);
    Task<List<MovieModel>> GetMoviesAsync();
    Task<MovieModel> GetMovieByIdAsync(int id);
    Task<MovieModel> UpdateMovieAsync(int id, MovieModel updatedMovie);
    Task<bool> DeleteMovieAsync(int id);
}
