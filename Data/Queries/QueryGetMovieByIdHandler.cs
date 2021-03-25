using Domain.Models.Request;
using MediatR;
using Domain.ViewModels;
using System.Threading.Tasks;
using System.Threading;
using Domain.IOptionClasses;
using Domain.Models;
using Microsoft.Extensions.Options;
using System.Net.Http;
using Newtonsoft.Json;
using Domain.Interfaces;
using System.Linq;

namespace Data.Queries
{
    public class QueryGetMovieByIdHandler : IRequestHandler<GetMovieByIdRequestFilters, GetMovieDetailsViewModel>
    {
        private readonly OpenAPISettings _openAPISettings;
        private readonly IStaticJsonService _staticJsonService;

        public QueryGetMovieByIdHandler(IOptions<OpenAPISettings> openAPISetting,
            IStaticJsonService staticJsonService)
        {
            _openAPISettings = openAPISetting.Value;
            _staticJsonService = staticJsonService;
        }

        public async Task<GetMovieDetailsViewModel> Handle(GetMovieByIdRequestFilters request, CancellationToken cancellationToken)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(CreateUri(request));
            if (response.IsSuccessStatusCode)
            {
                var jsonContent = await response.Content.ReadAsStringAsync();
                var deserialized = JsonConvert.DeserializeObject<GetMovieDetailsViewModel>(jsonContent);
                var moviesFromJson = _staticJsonService.GetMoviesFromJson("FavoriteMovies.json");
                if (moviesFromJson.FirstOrDefault(x => x.Id == deserialized.Id) != null)
                {
                    deserialized.IsFavorite = true;
                }
                return deserialized;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return new GetMovieDetailsViewModel
                {
                    Errors = JsonConvert.DeserializeObject<Error>(errorContent)
                };
            }
        }

        private string CreateUri(GetMovieByIdRequestFilters request)
        {
            var uri = _openAPISettings.UrlForMovieDetails + request.MovieId.ToString()
                + "?api_key=" + _openAPISettings.API_KEY;
            return uri;
        }
    }
}
