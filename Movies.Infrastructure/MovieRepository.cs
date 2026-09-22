using Microsoft.EntityFrameworkCore;
using Movies.Domain.Entities.DTOs;
using Movies.Domain.Entities.Models;
using Movies.Domain.Interfaces;
using Movies.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Infrastructure
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieDbContext _movieDbContext;
        public MovieRepository(MovieDbContext movie)
        {
            _movieDbContext = movie;
        }
        public async Task AddMovieAsync(Movie movie)
        {
            await _movieDbContext.Movies.AddAsync(movie);
            await _movieDbContext.SaveChangesAsync();
        }

        public async Task DeleteMovieByIdAsync(int id)
        {
            var movie=await _movieDbContext.Movies.FirstOrDefaultAsync(x => x.Id == id);

            _movieDbContext.Movies.Remove(movie);

            await _movieDbContext.SaveChangesAsync();

        }

        public async Task<ICollection<Movie>> GetAllMoviesAsync()
        {
            return await _movieDbContext.Movies.Include(x=>x.Studio).ToListAsync();
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            return await _movieDbContext.Movies
                .Include(m => m.Studio)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task UpdateMovieAsync(int id, UpdateMovieDto movie)
        {
            var existingMovie = await _movieDbContext.Movies
                  .Include(x => x.Studio)
                  .FirstOrDefaultAsync(m => m.Id == id);

            if (existingMovie == null)
            {
                throw new KeyNotFoundException($"movie by  {id}-id do not exists.");
            }

            existingMovie.StudioId = movie.StudioId;
            existingMovie.ReleaseYear = movie.ReleaseYear;
            existingMovie.Title = movie.Title;

            await _movieDbContext.SaveChangesAsync();
            
            


        }
    }
}
