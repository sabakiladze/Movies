using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Domain.Entities.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName{ get; set; }= null!;

        public ICollection<Movie> Movies = null!;

    }
}
