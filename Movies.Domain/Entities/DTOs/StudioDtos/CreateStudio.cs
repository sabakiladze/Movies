using Movies.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Domain.Entities.DTOs.StudioDtos
{
    public  class CreateStudio
    {
        public string Name { get; set; } = null!;
        public int CountryId { get; set; }
        public StudioDetails? StudioDetails { get; set; }
 

    }
}
