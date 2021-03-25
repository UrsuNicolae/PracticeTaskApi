using MediatR;
using Domain.ViewModels;

namespace Domain.Models.Request
{
    public class PageRequestFiltersForTopRatedMovies : PageRequestFilters, IRequest<GetPaginatedMoviesViewModel>
    {
    }
}
