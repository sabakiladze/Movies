using Movies.Domain.Entities.DTOs.MovieDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Interfaces
{
    public interface IMovieService
    {
        Task<ICollection<MovieDto>> GetAllMoviesAsync();
        Task AddMovieAsync(CreateMovieDto movieDto);

        Task<MovieDto> GetMovieByIdAsync(int id);

        Task DeleteMovieByIdAsync(int id);

        Task<MovieDto> UpdateMovieAsync(int id, UpdateMovieDto movieDto);

        Task<ICollection<SearchMovieDto>> SearchMoviesByCountryAsync(
                string countryName,
            int minimumYear,
            int maximumActorCount);
        Task<ICollection<SearchMovieDto>> SearchMoviesByStudioAsync(
            int year, string studioName, int minimumActorCount);
    }
}
