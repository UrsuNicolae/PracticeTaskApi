using Domain.Models;

namespace Domain.ViewModels
{
    public class GetMovieDetailsViewModel : Movie
    {
        public Error Errors { get; set; }

        public bool IsFavorite { get; set; } = false;
    }
}
