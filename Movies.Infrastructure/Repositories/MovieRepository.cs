using Microsoft.EntityFrameworkCore;
using Movies.Domain.Entities.DTOs.MovieDtos;
using Movies.Domain.Entities.Models;
using Movies.Domain.Interfaces;
using Movies.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Infrastructure.Repositories
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
        }

       

        public async Task DeleteMovieByIdAsync(int id)
        {
            var  movie=await _movieDbContext.Movies.FirstOrDefaultAsync(x => x.Id == id);

            _movieDbContext.Movies.Remove(movie);


        }

        public async Task<ICollection<Movie>> GetAllMoviesAsync()
        {
            return await _movieDbContext.Movies.Include(x => x.Studio).ToListAsync();
        }

        public async Task<int> GetCountOfMovies()
        {
            return  await _movieDbContext.Movies.AnyAsync() ? await _movieDbContext.Movies.MaxAsync(m => m.Id): 0;

        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            return await _movieDbContext.Movies.Include(m => m.Studio).Include(x=>x.Actors)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<ICollection<Movie>> SearchMoviesAdvancedAsync(int fromYear, int toYear, string countryName, string titleText, int minimumActorCount)
        {
            var movies = await _movieDbContext.Movies
                .Include(x => x.Studio)
                .Where(x => (fromYear <= x.ReleaseYear && toYear >= x.ReleaseYear)
                && x.Studio.Country.Name == countryName
                && x.Title.Contains(titleText)
                && x.Actors.Count >= minimumActorCount)
                .OrderByDescending(x => x.Actors.Count)
                .ThenByDescending(x => x.ReleaseYear)
                .ThenBy(x => x.Studio.Name)
                .ThenBy(x => x.Title)
                .ToListAsync();
            return movies;
        }

        public async Task<ICollection<Movie>> SearchMoviesByCountryAsync(string countryName, int minimumYear, int maximumActorCount)
        {
            var movie=await _movieDbContext.Movies
                .Include(x=>x.Studio)
                .Where(x=>x.ReleaseYear>=minimumYear 
                && x.Studio.Country.Name==countryName
                && x.Actors.Count<=maximumActorCount)
                .OrderBy(x=>x.Actors.Count())
                .ThenByDescending(x=>x.ReleaseYear)
                .ThenBy(x=>x.Title)
                .ToListAsync();

            return movie;
        }

        public async Task<ICollection<Movie>> SearchMoviesByStudioAsync(int year, string studioName, int minimumActorCount)
        {
            var movie = await _movieDbContext.Movies
                .Include(x=>x.Studio)
                .Include(x=>x.Actors)
                .Where(x=>x.ReleaseYear>=year &&
                x.Studio.Name==studioName && 
                x.Actors.Count>=minimumActorCount)
                .OrderByDescending(x=>x.ReleaseYear)
                .ThenBy(x=>x.Title)
                .ToListAsync();
            return movie;
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

            

        }

       
    }
}
