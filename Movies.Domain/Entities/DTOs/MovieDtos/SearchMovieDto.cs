using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Domain.Entities.DTOs.MovieDtos
{
    public class SearchMovieDto
    {
        public string ?Title { get; set; }
        public int ReleaseYear { get; set; }
        public string ?StudioName { get; set; }

        public string? CountryName { get; set; }

        public int ActorCount { get; set; }

        public override string? ToString()
        {
            return $"{Title} ({ReleaseYear}) - {StudioName}, {CountryName}, {ActorCount} actors";
        }
    }
}
