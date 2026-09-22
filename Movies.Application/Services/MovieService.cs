using Movies.Application.Interfaces;
using Movies.Domain.Entities.DTOs;
using Movies.Domain.Entities.Models;
using Movies.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        public MovieService(IMovieRepository movieRepository)
        {
             _movieRepository= movieRepository;
        }

        public async Task<MovieDto> UpdateMovieAsync(int id, UpdateMovieDto movie)
        {

            if (movie == null) throw new ArgumentNullException("invalid movie input");

            var existingMovie=await _movieRepository.GetMovieByIdAsync(id);
            if (id < 0) throw new ArgumentNullException("input by this id do not found");

            await _movieRepository.UpdateMovieAsync(id, movie);

            return new MovieDto
            {
                Id = existingMovie.Id,
                Title = existingMovie.Title,
                ReleaseYear = existingMovie.ReleaseYear,
                StudioName = existingMovie.Studio?.Name

            };



        }
        public async Task AddMovieAsync(CreateMovieDto movieDto)
        {

            if (movieDto == null)
            {
                throw new ArgumentNullException(nameof(movieDto));
            }
            if (string.IsNullOrWhiteSpace(movieDto.Title))
            {
                throw new ArgumentException("Movie title cannot be null or empty.", nameof(movieDto.Title));
            }
            if (movieDto.ReleaseYear < 0)
            {
                throw new ArgumentException("Movie release year cannot be negative.", nameof(movieDto.ReleaseYear));
            }
            if (movieDto.ReleaseYear > DateTime.Now.Year)
            {
                throw new ArgumentException("Movie release year cannot be from future.", nameof(movieDto.ReleaseYear));
            }
            if (movieDto.StudioId <= 0)
            {
                throw new ArgumentException("Movie studio ID must be a positive integer.", nameof(movieDto.StudioId));
            }

            var movie = new Movie
            {
                Title = movieDto.Title,
                ReleaseYear = movieDto.ReleaseYear,
                StudioId = movieDto.StudioId
            };


            await _movieRepository.AddMovieAsync(movie);
        }

        public async Task<ICollection<MovieDto>> GetAllMoviesAsync()
        {
            var movies = await _movieRepository.GetAllMoviesAsync();

            var movieDtos = movies.Select(m => new MovieDto
            {
                Title = m.Title,
                ReleaseYear = m.ReleaseYear,
                StudioName = m.Studio.Name,
            }).ToList();


            return movieDtos;
        }

        public async Task<MovieDto> GetMovieByIdAsync(int id)
        {
            var movie = await _movieRepository.GetMovieByIdAsync(id);

            if (movie == null)
            {
                throw new ArgumentException("Movie not found.", nameof(id));
            }

            return new MovieDto
            {
                Title = movie.Title,
                ReleaseYear = movie.ReleaseYear,
                StudioName = movie.Studio.Name,
            };
        }

        public Task DeleteMovieByIdAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("inputed id is invalid");
            _movieRepository.DeleteMovieByIdAsync(id);
            return Task.CompletedTask;
        }

        
    }
}
