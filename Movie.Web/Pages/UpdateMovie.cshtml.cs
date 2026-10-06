using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Movies.Application.Interfaces;
using Movies.Domain.Entities.DTOs.MovieDtos;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Movie.Web.Pages
{
    public class UpdateMovieModel : PageModel
    {


        private readonly IMovieService _movieService;
        public UpdateMovieModel(IMovieService movieService)
        {
            _movieService= movieService;
        }

        [BindProperty]
        public UpdateMovieDto? Movie { get; set; } = new UpdateMovieDto();

         //ატრიბუტი გამოიყენება იმისთვის,
                                           //რომ URL-იდან(Query String-იდან ან Route-იდან)
        // ამას დააკვირდი კიდევ კარგაად      //მოსული მონაცემები ავტომატურად ჩაიწეროს PageModel-ის
                                           //პროპერთიში GET მოთხოვნის დროსაც.    
        public int Id { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                var movie = await _movieService.UpdateMovieAsync(Id, Movie);
                TempData["SuccessMessage"] = "Movie updated successfully";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"]=ex.Message;
                return Page();
            }
            return RedirectToPage("/MoviePage");

        }
    }
}
