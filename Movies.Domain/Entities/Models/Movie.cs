using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Domain.Entities.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int ReleaseYear { get; set; }
        public int StudioId { get; set; }
        public Studio Studio { get; set; } = null!;
        public ICollection<Actor> Actors { get; set; } = new List<Actor>();
    }
}
