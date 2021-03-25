using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Response
{
    public class SetMovieToFavoriteResponse
    {
        public int Id { get; set; }

        public Error Error { get; set; }
    }
}
