using Movies.Domain.Entities.DTOs.StudioDtos;
using Movies.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Interfaces
{
    public interface IStudioService
    {
        Task<ICollection<ShowStudioDto>> GetAllStudios();
        Task AddStudio(CreateStudio dto);
        
    }
}
