using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Domain.Models.Request;
using Domain.ViewModels;

namespace PracticeTaskServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MovieController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetPaginatedPopularMovies([FromQuery] PageRequestFiltersForPopularMovies filters)
        {

            var result = await _mediator.Send(filters);

            if (result.Errors.Count > 0)
                return BadRequest(result);

            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetPaginatedTopRatedMovies([FromQuery]PageRequestFiltersForTopRatedMovies filters)
        {

            var result = await _mediator.Send(filters);

            if (result.Errors.Count > 0)
                return BadRequest(result);

            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetMovieById([FromQuery]int id)
        {
            var result = await _mediator.Send(new GetMovieByIdRequestFilters
            {
                MovieId = id
            });

            if (result.Errors != null)
                return BadRequest(result.Errors);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetMovieVideos([FromQuery]int id)
        {
            var result = await _mediator.Send(new GetVideosByMovieIdRequest
            {
                Id = id
            });

            if (result.Error != null)
                return BadRequest(result.Error);
            return Ok(result);
        }


        [HttpPut]
        public async Task<IActionResult> SetMovieToFavorite(int id)
        {

            var result = await _mediator.Send(new SetMovieToFavoriteViewModel
            {
                MovieId = id
            });

            if (result.Error != null)
                return BadRequest(result.Error);

            return Ok(result);
        }

        [HttpGet]
        public  Task<GetNrFavoriteMoviesResponseViewModel> GetNrOfFavoriteMovies()
        {
            var task = _mediator.Send(new GetNrFavoriteMoviesFilter());
            if (task.Result.Error != "")
                return Task.FromResult(task.Result);
            return Task.FromResult(task.Result);
        }
    }
}
