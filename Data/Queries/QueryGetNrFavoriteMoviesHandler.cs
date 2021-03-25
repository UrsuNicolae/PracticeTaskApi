using Domain.Interfaces;
using Domain.Models;
using Domain.Models.Request;
using Domain.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Queries
{
    public class QueryGetNrFavoriteMoviesHandler : IRequestHandler<GetNrFavoriteMoviesFilter, GetNrFavoriteMoviesResponseViewModel>
    {
        private readonly IStaticJsonService _staticJsonService;
        public QueryGetNrFavoriteMoviesHandler(IStaticJsonService staticJsonService)
        {
            _staticJsonService = staticJsonService;
        }
        public Task<GetNrFavoriteMoviesResponseViewModel> Handle(GetNrFavoriteMoviesFilter request, CancellationToken cancellationToken)
        {
            try
            {
                List<Movie> movies = _staticJsonService.GetMoviesFromJson("FavoriteMovies.json");
                return Task.FromResult(new GetNrFavoriteMoviesResponseViewModel
                {
                    Nr = movies.Count,
                    Error = ""
                });
            }
            catch(Exception e)
            {
                return Task.FromResult(new GetNrFavoriteMoviesResponseViewModel
                {
                    Nr = 0,
                    Error = e.Message
                });
            }

        }
    }
}
