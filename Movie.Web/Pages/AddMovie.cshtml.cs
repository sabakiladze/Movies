using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Movies.Domain.Entities.DTOs.MovieDtos;
using Movies.Domain.Interfaces;
using Movies.Infrastructure.Repositories;

namespace Movie.Web.Pages
{
    public class AddMovieModel : PageModel
    {
        private readonly IMovieRepository _movieRepository;
        public AddMovieModel(IMovieRepository movieRepositor)
        {
            _movieRepository = movieRepositor;
        }

        // [BindProperty] უზრუნველყოფს, რომ HTML ფორმიდან მონაცემები ავტომატურად ჩაიწეროს ამ ობიექტში
        // ეწერება ფროფერთის ამიტომ უნდა get and set.
        [BindProperty]
        public CreateMovieDto Input { get; set; } = new CreateMovieDto();
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // ისევე იმ გვერძე დაგვაბრუნებს რომელზედაც ვიმყოფებით
                // გვჭირდება იმისთვის რომ შეცდომები გამოგვიჩნდეს.
                // OnPostAsync მეთოდის IActionResult ტიპს აბრუნებს
                // მაგალითად როგორც კონტროლერში return ok();
                
                return Page();
            }

            try
            {
                var movie = new Movies.Domain.Entities.Models.Movie
                {
                    Title = Input.Title,
                    ReleaseYear = Input.ReleaseYear,
                    StudioId = Input.StudioId
                };

                await _movieRepository.AddMovieAsync(movie);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
            return RedirectToPage("/MoviePage");

        }
    }
}
