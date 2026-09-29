using Moq;
using Movies.Application.Services;
using Movies.Domain.Entities.DTOs.MovieDtos;
using Movies.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Test.ServicesTest
{
    public  class MockMovieService
    {
        [Fact]
        public async Task AddMovieAsync_ValidMovie_CallRepo()
        {
            //Arrange

            // ვქმნი იმ ობიექტებს რომლებსაც მე სერვისს უნდა გადავცე.
            // ანუ ვქმნი ამ ორი ოპბიექტის mock ებს, რომლებსაც მერე mock სერვისს გადავცემ.
            var mockRepository = new Mock<IMovieRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();


            // ვქმნის სერვისს და გადავცემ mock ების ობიექტებს.
            var movieService = new MovieService(mockRepository.Object, mockUnitOfWork.Object);


            // როგორც ვიცით, რეპოზიტორს გადაეცემა სუფთა მთავარი კლასისი ობიექტი, მაგრამ რადგან
            // ახლა ვაკეთებს მხოლოდ სერვისის ტესტირებას და არა რეპოზიტორის, ჩვენ უნდა ვუთხრათ
            // რომ მოიტყიოს და როდესაც რეპოზიტორს მიაკითხავს გამოაცხადოს როგორც შესრულებული.
            mockRepository.Setup(r => r.AddMovieAsync(It.IsAny<Movies.Domain.Entities.Models.Movie>()))
                 .Returns(Task.CompletedTask);


            var dto = new CreateMovieDto()  //არ დაგავიწყდეს რომ სერვისებს გადავცემ dto ებს, ხოლო რეპოზიტორებს მთავარ კლასებს.
            {
                Title = "Test Movie",
                ReleaseYear = 2000,
                StudioId = 1

            };

            //Act
            await movieService.AddMovieAsync(dto);

            //Assert
            mockRepository.Verify(repo => repo.AddMovieAsync(It.IsAny<Movies.Domain.Entities.Models.Movie> ()),
                Times.Once()); // ეს ამბობს რომ რეპოზიტორს უნდა მიაკითხოს ერთხელ მხოლოდ.
            // lambda მეთოდებს რატომ ვიყენებთ ?
        }
    }
}
