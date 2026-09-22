using Movies.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Domain.Entities.DTOs
{
    public  class UpdateMovieDto
    {
        public string Title { get; set; } = null!;
        public int ReleaseYear { get; set; }
        public int StudioId { get; set; }
        public Studio Studio { get; set; } = null!;
        public ICollection<Actor> Actors { get; set; } = new List<Actor>();
    }
}
