using Movies.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Domain.Entities.DTOs.StudioDtos
{
    public class ShowStudioDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string CountryName { get; set; } = null!;
        public string LicenseNumber { get; set; } = null!;
        public ICollection<Movie> Movies { get; set; } = null!;
    }
}
