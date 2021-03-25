using Data.Queries;
using Domain.Models;
using Domain.Models.Request;
using Domain.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PracticeTaskServer.Controllers;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class QueryTests
    {
        private readonly MovieController _movieController;
        private readonly Mock<IMediator> _mediator = new Mock<IMediator>();

        public QueryTests()
        {
            _movieController = new MovieController(_mediator.Object);
        }


        [Fact]
        public async Task GetPaginatedPopularMovies_ShouldReturn_OK_WhenProvidingCorrectParameters()
        {
            var request = new PageRequestFiltersForPopularMovies
            {
                Page = 3
            };

            _mediator.Setup(m => m.Send(It.IsAny<PageRequestFiltersForPopularMovies>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new GetPaginatedMoviesViewModel());

            var okResult = await _movieController.GetPaginatedPopularMovies(request);

            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task GetPaginatedPopularMovies_ShouldReturn_BadRequest_WhenProvidingInvalidParameters()
        {
            var request = new PageRequestFiltersForPopularMovies
            {
                Page = -22
            };
            var response = new GetPaginatedMoviesViewModel
            {
                Page = 0,
                Results = new List<GetMovieViewModel>(),
                Total_Pages = 0,
                Total_Results = 0,
                Errors = new List<string> { "page must be greater than 0" }
            };

            _mediator.Setup(m => m.Send(It.IsAny<PageRequestFiltersForPopularMovies>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

            var badResult = await _movieController.GetPaginatedPopularMovies(request);

            Assert.IsType<BadRequestObjectResult>(badResult);
        }

        [Fact]
        public async Task GetPaginatedTopRatedMovies_ShouldReturn_OK_WhenProvidingCorrectParameters()
        {
            var request = new PageRequestFiltersForTopRatedMovies
            {
                Page = 2
            };

            _mediator.Setup(m => m.Send(It.IsAny<PageRequestFiltersForTopRatedMovies>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new GetPaginatedMoviesViewModel());

            var okResult = await _movieController.GetPaginatedTopRatedMovies(request);

            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task GetPaginatedTopRatedMovies_ShouldReturn_BadRequest_WhenProvidingInvalidParameters()
        {
            var request = new PageRequestFiltersForTopRatedMovies
            {
                Page = -22
            };

            var response = new GetPaginatedMoviesViewModel
            {
                Page = 0,
                Results = new List<GetMovieViewModel>(),
                Total_Pages = 0,
                Total_Results = 0,
                Errors = new List<string> { "page must be greater than 0" }
            };

            _mediator.Setup(m => m.Send(It.IsAny<PageRequestFiltersForTopRatedMovies>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

            var badResult = await _movieController.GetPaginatedTopRatedMovies(request);

            Assert.IsType<BadRequestObjectResult>(badResult);
        }

        [Fact]
        public async Task GetMovieById_ShouldReturn_Ok_WhenProvidingCorrectId()
        {
            var response = new GetMovieDetailsViewModel
            {
                Errors = null,
                Adult = false,
                Backdrop_Path = "/nlCHUWjY9XWbuEUQauCBgnY8ymF.jpg",
                Belongs_to_collection = new MovieCollection
                {
                    Id = 8945,
                    Name = "Mad Max Collection",
                    Poster_Path = "/uuvSvLb3ntGA9B0wx2JskVDSuWi.jpg",
                    Backdrop_path = "/h4MNLYzr6Cr02iHv3Hpqb9lmTPv.jpg"
                },
                Budget = 150000000,
                Genres = new List<Genres>
                {
                    new Genres
                    {
                        Id = 28,
                        Name = "Action"
                    },
                    new Genres
                    {
                        Id = 12,
                        Name = "Adventure"
                    },
                    new Genres
                    {
                        Id = 878,
                        Name = "Science Fiction"
                    }
                },
                Homepage = "https://www.warnerbros.com/movies/mad-max-fury-road",
                Id = 76341,
                Imdb_Id = "tt1392190",
                Original_Language = "en",
                Original_Title = "Mad Max: Fury Road",
                Overview = "An apocalyptic story set in the furthest reaches of our planet, in a stark desert landscape where humanity is broken, and most everyone is crazed fighting for the necessities of life. Within this world exist two rebels on the run who just might be able to restore order.",
                Popularity = 60.884,
                Poster_Path = "/8tZYtuWezp8JbcsvHYO0O46tFbo.jpg",
                Production_Companies = new List<Production_companies>
                {
                new Production_companies
                {
                    Id = 2537,
                    Logo_Path = null,
                    Name = "Kennedy Miller Productions",
                    Originary_Country = "AU"
                },
                new Production_companies
                {
                    Id = 2537,
                    Logo_Path = null,
                    Name = "Kennedy Miller Productions",
                    Originary_Country = "AU"
                },
                new Production_companies
                {
                    Id = 2537,
                    Logo_Path = null,
                    Name = "Kennedy Miller Productions",
                    Originary_Country = "AU"
                },
                new Production_companies
                {
                    Id = 2537,
                    Logo_Path = null,
                    Name = "Kennedy Miller Productions",
                    Originary_Country = "AU"
                }
                },
                Production_Countries = new List<Production_countries>
                {
                    new Production_countries
                    {
                        Iso_3166_1= "AU",
                        Name = "Australia"
                    },
                    new Production_countries
                    {
                        Iso_3166_1= "US",
                        Name = "United States of America"
                    },
                    new Production_countries
                    {
                        Iso_3166_1= "ZA",
                        Name = "South Africa"
                    },
                },
                Release_Date = DateTime.Parse("2015-05-13"),
                Revenue = 378858340,
                Runtime = 121,
                Spoken_Languages = new List<Spoken_languages>
                {
                    new Spoken_languages
                    {
                        English_Name = "English",
                        Iso_639_1 = "en",
                        Name = "English"
                    }
                },
                Status = "Released",
                Tagline = "What a Lovely Day",
                Title = "Mad Max: Fury Road",
                Video = false,
                Vote_Average = 7.5,
                Vote_Count = 17782
            };
            _mediator.Setup(m => m.Send(It.IsAny<GetMovieByIdRequestFilters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

            var okResult = await _movieController.GetMovieById(76341);

            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task GetMovieById_ShouldReturn_BadRequest_WhenProvidingCorrectId()
        {

            var request = new GetMovieByIdRequestFilters
            {
                MovieId = -22
            };
            var response = new GetMovieDetailsViewModel
            {
                Errors = new Error
                {
                    Success = false,
                    Status_Code = 34,
                    Status_Message = "The resource you requested could not be found."
                },
                Adult = false,
                Backdrop_Path = null,
                Belongs_to_collection = null,
                Budget = 0,
                Genres = null,
                Homepage = null,
                Id = 0,
                Imdb_Id = null,
                Original_Language = null,
                Original_Title = null,
                Overview = null,
                Popularity = 0,
                Poster_Path = null,
                Production_Companies = null,
                Production_Countries = null,
                Release_Date = new DateTime(),
                Revenue = 0,
                Runtime = 0,
                Spoken_Languages = null,
                Status = null,
                Tagline = null,
                Title = null,
                Video = false,
                Vote_Average = 0,
                Vote_Count = 0
            };
            _mediator.Setup(m => m.Send(It.IsAny<GetMovieByIdRequestFilters>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            var badRequest = await _movieController.GetMovieById(76341);

            Assert.IsType<BadRequestObjectResult>(badRequest);
        }

        [Fact]
        public async Task GetVideosByMovieId_ShouldReturn_Ok_WhenProvidingCorrectId()
        {
            var response = new GetMovieVideosResultViewModel
            {
                Id = 123,
                Results = new List<GetMovieVideoViewModel>
                {
                    new GetMovieVideoViewModel
                    {
                        Id = "533ec652c3a3685448000101",
                        Iso_639_1= "en",
                        Iso_3166_1 = "US",
                        Key = "6WcJbPlAknw",
                        Name = "Lord of the Rings [1978] - One to Rule Them All",
                        Site = "YouTube",
                        Size = 360,
                        Type = "Clip"
                    },
                    new GetMovieVideoViewModel
                    {
                        Id = "5f7ad63c7b7b4d003a7584f0",
                        Iso_639_1= "en",
                        Iso_3166_1 = "US",
                        Key = "9qhL2_UxXM0",
                        Name = "The Lord of the Rings (1978) Trailer.",
                        Site = "YouTube",
                        Size = 1080,
                        Type = "Trailer"
                    }
                },
                Error = null
            };
            _mediator.Setup(m => m.Send(It.IsAny<GetVideosByMovieIdRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

            var okResult = await _movieController.GetMovieVideos(123);

            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task GetVideosByMovieId_ShouldReturn_BadRequest_WhenProvidingInvalidId()
        {
            var response = new GetMovieVideosResultViewModel
            {
                Error = new Error
                {
                    Success = false,
                    Status_Code = 34,
                    Status_Message = "The resource you requested could not be found."
                },
            };
            _mediator.Setup(m => m.Send(It.IsAny<GetVideosByMovieIdRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

            var badResult = await _movieController.GetMovieVideos(-123);

            Assert.IsType<BadRequestObjectResult>(badResult);
        }

        [Fact]
        private void GetFavoriteNr_ShouldReturn_TaskGetNrFavoriteMoviesResponseViewModel()
        {
            _mediator.Setup(m => m.Send(It.IsAny<GetNrFavoriteMoviesFilter>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new GetNrFavoriteMoviesResponseViewModel()));
            var result = _movieController.GetNrOfFavoriteMovies();
            Assert.IsType<GetNrFavoriteMoviesResponseViewModel>(result.Result);
        }
    }
}
