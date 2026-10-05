using Movies.Domain.Entities.DTOs.MovieDtos;
using Movies.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Domain.Interfaces
{
    public interface IMovieRepository
    {
        Task<ICollection<Movie>> GetAllMoviesAsync();
        Task AddMovieAsync(Movie movie);

        Task<Movie> GetMovieByIdAsync(int id);

        Task DeleteMovieByIdAsync(int id);
        Task UpdateMovieAsync(int id, UpdateMovieDto movie);

        
        Task<ICollection<Movie>> SearchMoviesAdvancedAsync(
            int fromYear,
            int toYear,
            string countryName,
            string titleText,
            int minimumActorCount);
        Task<ICollection<Movie>> SearchMoviesByCountryAsync(
           string countryName,
           int minimumYear,
           int maximumActorCount);

        Task<ICollection<Movie>> SearchMoviesByStudioAsync(
            int year,
            string studioName,
            int minimumActorCount);

        Task<int> GetCountOfMovies();
    }
}

