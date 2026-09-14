using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Domain.Entities.Models
{
    public class StudioDetails
    {
        public int Id { get; set; }
        public int StudioId { get; set; }
        public Studio Studio { get; set; } = null!;
        public string LicenseNumber { get; set; } = null!;
    }
}
