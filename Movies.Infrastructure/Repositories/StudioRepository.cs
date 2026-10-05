using Microsoft.EntityFrameworkCore;
using Movies.Domain.Entities.DTOs.StudioDtos;
using Movies.Domain.Entities.Models;
using Movies.Domain.Interfaces;
using Movies.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Infrastructure.Repositories
{
    public class StudioRepository : IStudioRepository
    {
        private readonly MovieDbContext _movieDbContext;
        public StudioRepository(MovieDbContext movieDbContext)
        {
            _movieDbContext = movieDbContext;
        }
        public async Task CreateStudio(Studio dto)
        {
            await _movieDbContext.Studios.AddAsync(dto);
        }

        public async Task<ICollection<Studio>> GetAllStudios()
        {
            return await _movieDbContext.Studios
                .Include(x=>x.Movies).Include(x=>x.Country)
                .ToListAsync();
        }
    }
}
