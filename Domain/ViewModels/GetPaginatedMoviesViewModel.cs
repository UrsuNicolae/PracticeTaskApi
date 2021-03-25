using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ViewModels
{
    public class GetPaginatedMoviesViewModel
    {
        public int Page { get; set; }

        public List<GetMovieViewModel> Results { get; set; } = new List<GetMovieViewModel>();

        public int Total_Pages { get; set; }

        public int Total_Results { get; set; }

        public List<string> Errors { get; set; } = new List<string>();
    }
}
