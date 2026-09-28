using Movies.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Domain.Interfaces
{
    public  interface IActorRepository
    {
        Task AddActorAsync(Actor actor);
        Task<ICollection<Actor>> GetAllActorsAsync();
        Task<Actor> GetActorByIdAsync(int id);
        Task UpdateActorAsync(int id, Actor actor);
        Task DeleteActorAsync(int id);
        Task UpdateActorMoviesAsync(int actorId, ICollection<int> movieIds);
    }
}
