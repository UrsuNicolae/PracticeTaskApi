using Domain.Models;
using Domain.Models.Response;
using Domain.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PracticeTaskServer.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class HandlerTests
    {
        private readonly MovieController _movieController;
        private readonly Mock<IMediator> _mediator = new Mock<IMediator>();

        public HandlerTests()
        {
            _movieController = new MovieController(_mediator.Object);
        }

        [Fact]
        public async Task SetMovieToFavorite_ShouldReturn_Ok_WhenProvidingCorrectId()
        {
            var response = new SetMovieToFavoriteResponse
            {
                Id = 76341,
                Error = null
            };
            _mediator.Setup(m => m.Send(It.IsAny<SetMovieToFavoriteViewModel>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

            var okResult = await _movieController.SetMovieToFavorite(76341);

            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task SetMovieToFavorite_ShouldReturn_BadRequest_WhenProvidingIncorrectId()
        {
            var response = new SetMovieToFavoriteResponse
            {
                Id = 0,
                Error = new Error
                {
                    Success = false,
                    Status_Code = 34,
                    Status_Message = "The resource you requested could not be found."
                },
            };
            _mediator.Setup(m => m.Send(It.IsAny<SetMovieToFavoriteViewModel>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

            var badResult = await _movieController.SetMovieToFavorite(-2);

            Assert.IsType<BadRequestObjectResult>(badResult);
        }

    }
}
