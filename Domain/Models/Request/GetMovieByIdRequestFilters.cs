using MediatR;
using Domain.ViewModels;

namespace Domain.Models.Request
{
    public class GetMovieByIdRequestFilters : IRequest<GetMovieDetailsViewModel>
    {
        public int MovieId { get; set; }
    }
}
