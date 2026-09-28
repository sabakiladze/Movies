using Movies.Application.Interfaces;
using Movies.Domain.Entities.DTOs.ActorDtos;
using Movies.Domain.Entities.Models;
using Movies.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Services
{
    public class ActorService : IActorService
    {
        private readonly IActorRepository _actorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ActorService(IActorRepository actorRepository, IUnitOfWork unitOfWork)
        {
            _actorRepository = actorRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task DeleteActorAsync(int id)
        {

            if (id <= 0)
            {
                throw new ArgumentException("Invalid actor ID.");
            }
            await _actorRepository.DeleteActorAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }
        public  async Task AddActorAsync(CreateActorDto actorDto)
        {
            if (actorDto == null)
            {
                throw new ArgumentNullException(nameof(actorDto));
            }
            var actor = new Actor
            {
                FirstName = actorDto.FirstName,
                LastName = actorDto.LastName
            };
            await _actorRepository.AddActorAsync(actor);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<ActorDto> GetActorAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid actor ID.");
            }

            var actor = await _actorRepository.GetActorByIdAsync(id);

            var actorDto = new ActorDto
            {
                Id = actor.Id,
                FirstName = actor.FirstName,
                LastName = actor.LastName,

                MvoieTiles = actor.Movies.Select(m => m.Title).ToList()

            };

            return actorDto;

        }

        public async Task<ICollection<ActorDto>> GetAllActorsAsync()
        {
            var actors = await _actorRepository.GetAllActorsAsync();

            var actorDtos = actors.Select(a => new ActorDto
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,

                MvoieTiles = a.Movies.Select(m => m.Title).ToList()

            }).ToList();

            return actorDtos;
        }

        public async Task UpdateActorAsync(int id, UpdateActorDto actorDto)
        {
            if (actorDto == null)
            {
                throw new ArgumentNullException(nameof(actorDto));
            }
            var actor = new Actor
            {
                FirstName = actorDto.FirstName,
                LastName = actorDto.LastName
            };
            await _actorRepository.UpdateActorAsync(id, actor);
            await _unitOfWork.SaveChangesAsync();
        }

        public  async Task UpdateActorMoviesAsync(int actorId, UpdateActorMovieDto updateActorMovieDto)
        {
            await _actorRepository.UpdateActorMoviesAsync(actorId, updateActorMovieDto.MovieIds);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
