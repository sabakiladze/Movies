using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Domain.Entities.DTOs.ActorDtos
{
    public class UpdateActorDto
    {
        public string? FirstName { get; set; } 
        public string? LastName { get; set; }
    }
}
