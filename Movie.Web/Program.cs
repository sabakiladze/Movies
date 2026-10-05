using Movies.Application.Interfaces;
using Movies.Application.Services;
using Movies.Domain.Interfaces;
using Movies.Infrastructure.Data;
using Movies.Infrastructure.Repositories;
using System.Threading.Tasks;

namespace Movie.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddDbContext<MovieDbContext>();
            builder.Services.AddScoped<IMovieRepository, MovieRepository>();
            builder.Services.AddScoped<IMovieService, MovieService>();
            builder.Services.AddScoped<IActorRepository, ActorRepository>();
            builder.Services.AddScoped<IActorService, ActorService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

           

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<MovieDbContext>();

                // 1. ვრწმუნდებით, რომ სტუდიო (StudioId = 1) არსებობს (ადრე თუ არ შეგვიქმნია)
                if (!context.Studios.Any(s => s.Id == 1))
                {
                    var studio = new Movies.Domain.Entities.Models.Studio
                    {
                        Id = 1,
                        Name = "Warner Bros."
                    };
                    context.Studios.Add(studio);
                    context.SaveChanges();
                }

                // 2. ვქმნით ახალ ფილმს, რომელსაც ექნება ახალი Id (მაგალითად: 2) და მივთითებთ იმავე სტუდიას
                if (!context.Movies.Any(m => m.Id == 2))
                {
                    // იქვე ვქმნით მსახიობებსაც, რომლებსაც ასევე ხელით ვუწერთ Id-ებს
                    var actor1 = new Movies.Domain.Entities.Models.Actor
                    {
                        Id = 2, // რადგან 1 შეიძლება უკვე დაკავებული იყოს ლეონარდოსთან
                        FirstName = "Christian",
                        LastName = "Bale"
                    };

                    var actor2 = new Movies.Domain.Entities.Models.Actor
                    {
                        Id = 3,
                        FirstName = "Heath",
                        LastName = "Ledger"
                    };

                    var movie = new Movies.Domain.Entities.Models.Movie
                    {
                        Id = 2,                      // ფილმის ხელით მითითებული ID
                        Title = "The Dark Knight",
                        ReleaseYear = 2008,
                        StudioId = 1,                // უკავშირდება არსებულ სტუდიას
                        Actors = new List<Movies.Domain.Entities.Models.Actor>
            {
                actor1,
                actor2
            }
                    };

                    context.Movies.Add(movie);
                    context.SaveChanges();

                    Console.WriteLine("ფილმი ახალი მსახიობებით წარმატებით დაემატა!");
                }
            }

            app.Run();
        }
    }
}
