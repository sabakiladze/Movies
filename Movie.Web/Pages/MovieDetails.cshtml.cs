using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Movies.Application.Interfaces;
using Movies.Domain.Entities.DTOs.MovieDtos;

namespace Movie.Web.Pages
{
    public class MovieDetailsModel : PageModel
    {
        private readonly IMovieService _movieService;
        public MovieDetailsModel(IMovieService movieService)
        {
            _movieService = movieService;
        }
        public MovieDto? Movie { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Movie = await _movieService.GetMovieByIdAsync(id);

            if(Movie is null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
