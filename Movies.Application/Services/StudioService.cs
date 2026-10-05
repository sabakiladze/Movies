using Movies.Application.Interfaces;
using Movies.Domain.Entities.DTOs.StudioDtos;
using Movies.Domain.Entities.Models;
using Movies.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Services
{
    public class StudioService : IStudioService
    {
        private readonly IStudioRepository _studioRepository;
        private readonly IUnitOfWork _unitOfWork;
        public StudioService(IStudioRepository studio, IUnitOfWork work)
        {
            _studioRepository = studio;
            _unitOfWork = work;
        }
        public async Task AddStudio(CreateStudio dto)
        {
            if (dto is null)
            {
                throw new ArgumentNullException(nameof(dto));
            }
            var studio = new Studio
            {
                Name = dto.Name,
                CountryId = dto.CountryId,
                StudioDetails = dto.StudioDetails,
                Movies=new List<Movie>()
            };
            await _studioRepository.CreateStudio(studio);

            await _unitOfWork.SaveChangesAsync();
        }

              
        public async Task<ICollection<ShowStudioDto>> GetAllStudios()
        {
            var studios= await _studioRepository.GetAllStudios();
            var studiosDtos=studios.Select(x=>
            new ShowStudioDto
            {
                Id=x.Id,
                Name=x.Name,
                CountryName=x.Country.Name,
                //LicenseNumber=x.StudioDetails.LicenseNumber,
                Movies=x.Movies
            }).ToList();

            return studiosDtos;
        }
    }
}
