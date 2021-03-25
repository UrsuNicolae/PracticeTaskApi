using Domain.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Request
{
    public class GetVideosByMovieIdRequest: IRequest<GetMovieVideosResultViewModel>
    {
        public int Id { get; set; }
    }
}
