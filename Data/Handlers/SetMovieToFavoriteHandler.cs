using MediatR;
using Domain.ViewModels;
using Domain.IOptionClasses;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System.Threading;
using Domain.Models.Response;
using System.Net.Http;
using Newtonsoft.Json;
using Domain.Models;
using System.Text;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Domain.Interfaces;

namespace Data.Handlers
{
    public class SetMovieToFavoriteHandler : IRequestHandler<SetMovieToFavoriteViewModel, SetMovieToFavoriteResponse>
    {
        private readonly OpenAPISettings _openAPISettings;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly IStaticJsonService _staticJsonService;

        public SetMovieToFavoriteHandler(IOptions<OpenAPISettings> openAPISettings,
            IHostEnvironment hostEnvironment,
            IStaticJsonService staticJsonService)
        {
            _openAPISettings = openAPISettings.Value;
            _hostEnvironment = hostEnvironment;
            _staticJsonService = staticJsonService;
        }

        public async Task<SetMovieToFavoriteResponse> Handle(SetMovieToFavoriteViewModel request, CancellationToken cancellationToken)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(CreateUri(request));
            if (response.IsSuccessStatusCode)
            {
                var jsonContent = await response.Content.ReadAsStringAsync();
                var deserializedObj = JsonConvert.DeserializeObject<GetMovieDetailsViewModel>(jsonContent);
                try
                {
                    StoreMovie(deserializedObj);
                }
                catch(Exception e)
                {
                    return new SetMovieToFavoriteResponse
                    {
                        Id = deserializedObj.Id,
                        Error = new Error
                        {
                            Status_Message = e.Message
                        }
                    };
                }
                return new SetMovieToFavoriteResponse
                {
                    Id = deserializedObj.Id,
                    Error = null
                };
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return new SetMovieToFavoriteResponse
                {
                    Error = JsonConvert.DeserializeObject<Error>(errorContent)
                };
            }
        }


        private string CreateUri(SetMovieToFavoriteViewModel request)
        {
            string uri = _openAPISettings.UrlForMovieDetails + request.MovieId.ToString()
                + "?api_key=" + _openAPISettings.API_KEY;
            return uri;
        }

        private void StoreMovie(Movie movie)
        {
            string[] folders = _hostEnvironment.ContentRootPath.Split('\\');
            var rootPath = new StringBuilder();
            for (int i = 0; i < folders.Length - 1; i++)
            {
                rootPath.Append(folders[i] + "\\");
            }
            rootPath.Append("Domain\\Static\\FavoriteMovies.json");
            try
            {
                List<Movie> movies = _staticJsonService.GetMoviesFromJson("FavoriteMovies.json");
                if (movies.FirstOrDefault(x => x.Id == movie.Id) == null)
                {
                    movies.Add(movie);
                    string json = JsonConvert.SerializeObject(movies);
                    File.WriteAllText(rootPath.ToString(), json);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
