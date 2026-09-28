using Movies.Domain.Entities.DTOs.ActorDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Interfaces
{
    public interface IActorService
    {
        Task AddActorAsync(CreateActorDto actorDto);
        Task<ActorDto> GetActorAsync(int id);
        Task<ICollection<ActorDto>> GetAllActorsAsync();
        Task UpdateActorAsync(int id, UpdateActorDto actorDto);
        Task UpdateActorMoviesAsync(int actorId, UpdateActorMovieDto updateActorMovieDto);
        Task DeleteActorAsync(int id);
    }
}
