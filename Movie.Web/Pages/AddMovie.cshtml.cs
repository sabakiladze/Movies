using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Movies.Application.Interfaces;
using Movies.Domain.Entities.DTOs.MovieDtos;
using Movies.Domain.Interfaces;
using Movies.Infrastructure.Repositories;

namespace Movie.Web.Pages
{
    public class AddMovieModel : PageModel
    {
        private readonly IMovieService _movieService;
        public AddMovieModel(IMovieService movieService)
        {
            _movieService = movieService;
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
                
                await _movieService.AddMovieAsync(Input);

            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;

                ModelState.AddModelError(string.Empty, $"შეცდომა: {errorMessage}");
                return Page();
            }
            return RedirectToPage("/MoviePage");

        }
    }
}
