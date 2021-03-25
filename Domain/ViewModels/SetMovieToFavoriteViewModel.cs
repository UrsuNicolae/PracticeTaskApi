using System;
using MediatR;
using Domain.Models.Response;

namespace Domain.ViewModels
{
    public class SetMovieToFavoriteViewModel : IRequest<SetMovieToFavoriteResponse>
    {
        public int MovieId { get; set; }
    }
}
