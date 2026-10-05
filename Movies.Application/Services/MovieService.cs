using Movies.Application.Interfaces;
using Movies.Domain.Entities.DTOs.MovieDtos;
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
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly IStudioRepository _studioRepository;

        public MovieService(IMovieRepository movieRepository, IUnitOfWork iUnitOfWork, IStudioRepository studioRepository)
        {
             _movieRepository= movieRepository;
            _iUnitOfWork= iUnitOfWork;
            _studioRepository= studioRepository;
        }

        public async Task<MovieDto> UpdateMovieAsync(int id, UpdateMovieDto movie)
        {

            if (movie == null) throw new ArgumentNullException("invalid movie input");

            var existingMovie=await _movieRepository.GetMovieByIdAsync(id);
            if (id < 0) throw new ArgumentNullException("input by this id do not found");

            await _movieRepository.UpdateMovieAsync(id, movie);

            await _iUnitOfWork.SaveChangesAsync();

            return new MovieDto
            {
                Id = existingMovie.Id,
                Title = existingMovie.Title,
                ReleaseYear = existingMovie.ReleaseYear,
                //StudioName = existingMovie.Studio?.Name

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
            if(await _studioRepository.SearchStudioById(movieDto.StudioId) == null)
            {
                throw new ArgumentException($"Studio with ID {movieDto.StudioId} does not exist.", nameof(movieDto.StudioId));
            }

            var idcount = Convert.ToInt32(await _movieRepository.GetCountOfMovies());
            var movie = new Movie
            {
                
                Id = idcount+1,
                Title = movieDto.Title,
                ReleaseYear = movieDto.ReleaseYear,
                StudioId = movieDto.StudioId
            };


            await _movieRepository.AddMovieAsync(movie);
            await _iUnitOfWork.SaveChangesAsync();

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
                ActorName = movie.Actors.Select(x => $"{x.FirstName}  {x.LastName}").ToList()
            };
        }

        public async Task DeleteMovieByIdAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("inputed id is invalid");
           await  _movieRepository.DeleteMovieByIdAsync(id);

            await _iUnitOfWork.SaveChangesAsync();
        }

        public async Task<ICollection<SearchMovieDto>> SearchMoviesByStudioAsync(
             int year, string studioName, int minimumActorCount)
        {
            #region validation
            if (year < 0)
            {
                throw new ArgumentException("Year cannot be negative.", nameof(year));
            }
            if (string.IsNullOrWhiteSpace(studioName))
            {
                throw new ArgumentException("Studio name cannot be null or empty.", nameof(studioName));
            }
            if (minimumActorCount < 0)
            {
                throw new ArgumentException("Minimum actor count cannot be negative.", nameof(minimumActorCount));
            }

            #endregion


            var movies = await _movieRepository.SearchMoviesByStudioAsync(year, studioName, minimumActorCount);

            return movies.Select(MapMovieDTO).ToList();
        }




        public async Task<ICollection<SearchMovieDto>> SearchMoviesByCountryAsync(
                string countryName,
            int minimumYear,
            int maximumActorCount)
        {
           
            if (minimumYear < 0)
            {
                throw new ArgumentException("Year cannot be negative.", nameof(minimumYear));
            }
            if (string.IsNullOrWhiteSpace(countryName))
            {
                throw new ArgumentException("Studio name cannot be null or empty.", nameof(countryName));
            }
            if (maximumActorCount < 0)
            {
                throw new ArgumentException("Minimum actor count cannot be negative.", nameof(maximumActorCount));
            }

            


            var movies = await _movieRepository.SearchMoviesByCountryAsync(countryName, minimumYear, maximumActorCount);
            return movies.Select(x=>MapMovieDTO(x)).ToList();
        }

        private static SearchMovieDto MapMovieDTO(Movie movie)
        {
            return new SearchMovieDto
            {
                Title = movie.Title,
                ReleaseYear = movie.ReleaseYear,
                StudioName = movie.Studio.Name,
                CountryName = movie.Studio.Country.Name,
                ActorCount = movie.Actors.Count
            };

        }
    }

}
