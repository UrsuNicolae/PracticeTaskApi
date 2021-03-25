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
using System.Collections.Generic;

namespace Data.Queries
{
    public class QueryGetMovieVideosHandler : IRequestHandler<GetVideosByMovieIdRequest, GetMovieVideosResultViewModel>
    {
        private readonly OpenAPISettings _openAPISettings;

        public QueryGetMovieVideosHandler(IOptions<OpenAPISettings> openAPISetting)
        {
            _openAPISettings = openAPISetting.Value;
        }

        public async Task<GetMovieVideosResultViewModel> Handle(GetVideosByMovieIdRequest request, CancellationToken cancellationToken)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(CreateUri(request));
            if (response.IsSuccessStatusCode)
            {
                var jsonContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<GetMovieVideosResultViewModel>(jsonContent);

            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return new GetMovieVideosResultViewModel
                {
                    Error = JsonConvert.DeserializeObject<Error>(errorContent)
                };
            }
        }

        private string CreateUri(GetVideosByMovieIdRequest request)
        {
            var uri = _openAPISettings.UrlForMovieDetails + request.Id.ToString()
                + "/videos"
                + "?api_key=" + _openAPISettings.API_KEY;
            return uri;
        }
    }
}
