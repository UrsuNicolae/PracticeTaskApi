using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class MovieCollection
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Poster_Path { get; set; }

        public string Backdrop_path { get; set; }
    }
}
