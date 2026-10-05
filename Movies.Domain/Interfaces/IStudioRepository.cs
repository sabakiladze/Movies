using Movies.Domain.Entities.DTOs.StudioDtos;
using Movies.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Domain.Interfaces
{
    public interface IStudioRepository
    {
        Task<ICollection<Studio>> GetAllStudios();
        Task CreateStudio(Studio dto);

    }
}
