using MediatR;
using System.Collections.Generic;
using System.Net.Http;
using Domain.Models.Request;
using Domain.ViewModels;
using System.Threading.Tasks;
using System.Threading;
using Domain.IOptionClasses;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Linq;

namespace Data.Queries
{
    public class QueryGetPaginatedTopRatedMovies : IRequestHandler<PageRequestFiltersForTopRatedMovies, GetPaginatedMoviesViewModel>
    {
        private readonly OpenAPISettings _openAPISettings;

        public QueryGetPaginatedTopRatedMovies(IOptions<OpenAPISettings> openAPISettings)
        {
            _openAPISettings = openAPISettings.Value;
        }

        public async Task<GetPaginatedMoviesViewModel> Handle(PageRequestFiltersForTopRatedMovies request, CancellationToken cancellationToken)
        {
            
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(CreateUri(request));
            if (response.IsSuccessStatusCode)
            {
                var jsonContent = await response.Content.ReadAsStringAsync();
                var paginatedViewModel = JsonConvert.DeserializeObject<GetPaginatedMoviesViewModel>(jsonContent);

                paginatedViewModel.Results = Filter(paginatedViewModel.Results, request);
                return paginatedViewModel;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<GetPaginatedMoviesViewModel>(errorContent);
            }
        }

        private string CreateUri(PageRequestFiltersForTopRatedMovies request)
        {
            var uri = _openAPISettings.UrlForTopRatedMovies 
                + _openAPISettings.API_KEY + "&page=" + request.Page.ToString()
                + "&language" + request.Language + "&include_adult" + request.Include_Adult
                + "&include_video" + request.Include_Video;
            return uri;
        }

        private List<GetMovieViewModel> Filter(List<GetMovieViewModel> unfilteredResult,
            PageRequestFiltersForTopRatedMovies request)
        {
            List<GetMovieViewModel> filterdResult = unfilteredResult;
            if (request.MostPopular)
            {
                filterdResult = filterdResult.OrderBy(x => x.Vote_Count).Reverse().ToList();
            }
            if (request.MostIncome)
            {
                filterdResult = filterdResult.OrderBy(x => x.Revenue).Reverse().ToList();
            }
            if (request.Latest)
            {
                filterdResult = filterdResult.OrderBy(x => x.Release_Date).ToList();
            }

            return filterdResult;
        }
    }
}
