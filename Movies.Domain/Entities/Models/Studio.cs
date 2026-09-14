using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Domain.Entities.Models
{
    public class Studio
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CountryId { get; set; }
        public Country Country { get; set; } = null!;

        public StudioDetails StudioDetails { get; set; } = null!;
        public ICollection<Movie> Movies { get; set; } = null!;
    }
}
