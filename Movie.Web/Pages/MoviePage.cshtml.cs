using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Movies.Domain.Entities.DTOs.MovieDtos;
using Movies.Domain.Interfaces;
using System.Threading.Tasks;

namespace Movie.Web.Pages
{
    public class InMoviePageModel : PageModel
    {
        private readonly IMovieRepository _movieRepository;
        public InMoviePageModel(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public ICollection<MovieDto> Movies { get; set; } = new List<MovieDto>();

        public async Task OnGetAsync()

        {
            var movies = await _movieRepository.GetAllMoviesAsync();
            var moviesDto = movies.Select(x => 
            new MovieDto 
            { Id = x.Id, 
                ReleaseYear = x.ReleaseYear,
                StudioName = x.Studio?.Name,
                Title = x.Title }).ToList();


            foreach(var movie in moviesDto)
            {
                Movies.Add(movie);
            }
        }
    }
}
