using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.IOptionClasses
{
    public class OpenAPISettings
    {
        public string UrlForPopularMovies { get; set; }

        public string UrlForTopRatedMovies { get; set; }

        public string UrlForMovieDetails { get; set; }

        public string API_KEY { get; set; }
    }
}
