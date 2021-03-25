using MediatR;
using Domain.ViewModels;

namespace Domain.Models.Request
{
    public class PageRequestFiltersForPopularMovies: PageRequestFilters, IRequest<GetPaginatedMoviesViewModel>
    {
    }
}
