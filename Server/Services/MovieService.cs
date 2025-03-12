using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackMyMedia.Server.Data;
using TrackMyMedia.Shared.Models;

public class MovieService : IMovieService
{
    private readonly TrackMyMediaDbContext Context;

    public MovieService(TrackMyMediaDbContext context)
    {
        Context = context;
    }

    public async Task<MovieModel> CreateMovieAsync(MovieModel movie)
    {
        movie.DateAdded= DateTime.Now;
        Context.Movies.Add(movie);
        await Context.SaveChangesAsync();
        return movie;
    }

    public async Task<List<MovieModel>> GetMoviesAsync()
    {
        return await Context.Movies.ToListAsync();
    }

    public async Task<MovieModel> GetMovieByIdAsync(int id)
    {
        return await Context.Movies
            .FirstOrDefaultAsync(m => m.MediaId == id);
    }

    public async Task<MovieModel> UpdateMovieAsync(int id, MovieModel updatedMovie)
    {
        var existingMovie = await Context.Movies.FindAsync(id);
        if (existingMovie == null)
            return null; 

        // Update fields
        existingMovie.Title = updatedMovie.Title;
        existingMovie.Description = updatedMovie.Description;
        existingMovie.ReleaseDate = updatedMovie.ReleaseDate;
        existingMovie.MyRating = updatedMovie.MyRating;

        await Context.SaveChangesAsync();
        return existingMovie;
    }

    public async Task<bool> DeleteMovieAsync(int id)
    {
        var movie = await Context.Movies.FindAsync(id);
        if (movie == null)
            return false; // Movie not found

        Context.Movies.Remove(movie);
        await Context.SaveChangesAsync();
        return true;
    }
}
