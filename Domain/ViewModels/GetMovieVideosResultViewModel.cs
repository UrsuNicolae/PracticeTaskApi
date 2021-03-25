using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ViewModels
{
    public class GetMovieVideosResultViewModel
    {
        public int Id { get; set; }

        public List<GetMovieVideoViewModel> Results { get; set; }

        public Error Error { get; set; }
    }
}
