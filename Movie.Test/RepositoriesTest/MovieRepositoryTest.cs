using Microsoft.EntityFrameworkCore;
using Movies.Domain.Entities.Models;
using Movies.Infrastructure.Data;
using Movies.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Test.RepositoriesTest
{
    public class MovieRepositoryTest
    {
        //arrange
        private MovieDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<MovieDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            return new MovieDbContext(options);
        }

        [Fact]
        public async Task GetAllMoviesAsync_ReturnAllMovies() //რას ვტესტავ-რა მეთოდს ველოდები
        {
            // arrange მომზადებბა
            var context= CreateContext(); // აბრუნებს ყალბ ბაზას.

            var country = new Country { Id = 1, Name = "USA" };
            var studio= new Studio { Id = 1, Name="Marvel", CountryId = country.Id, Country=country };

            context.Countrys.Add(country);
            context.Studios.Add(studio);

            var movie1 = new Movies.Domain.Entities.Models.Movie { Id = 1, Title = "Avangers", ReleaseYear = 2012, Studio = studio, StudioId = studio.Id };
            var movie2= new Movies.Domain.Entities.Models.Movie { Id = 2, Title = "Avangers endgame", ReleaseYear = 2019, Studio = studio, StudioId = studio.Id }

            context.Movies.AddRange(movie1);
            await context.SaveChangesAsync();

            var sut = new MovieRepository(context); // შევქმენით MovieRepository რომელსაც გადავცემთ ყალბ ბაზას.
                                                    // რეპოზიტორის დეპენდენცი ინჯეცტიონ ით ხოიმ გადავცემდი ბაზას და ეგ პონტია აქაც.
                                                    // context ყალბი ბაზა

            // Act 
            var result= await sut.GetAllMoviesAsync();   // ვიძახებ ამ რეპოზიტორის მეთოდს და ვინახავ შედეგს. რეპოზიტორი წამოიღებს ყალბი ბაზის მონაცემებს


            //Assert

            Assert.Equal(2, result.Count);

            // აქ მოვიძიო რა ტიპის Asserts შემიძ₾ია კიდევ.
        }




    } 
}
