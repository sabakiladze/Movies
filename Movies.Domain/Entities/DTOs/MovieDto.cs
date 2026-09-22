using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Domain.Entities.DTOs
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int ReleaseYear { get; set; }
        public string ?StudioName { get; set; }

        public override string? ToString()
        {
            return $"MovieDTO: Id={Id}, Title={Title}, ReleaseYear={ReleaseYear}, StudioName={StudioName}";
        }
    }
}
