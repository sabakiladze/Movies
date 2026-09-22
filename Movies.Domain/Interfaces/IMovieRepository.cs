using Movies.Domain.Entities.DTOs;
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
    }
}

