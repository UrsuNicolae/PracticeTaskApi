using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IStaticJsonService
    {
        List<Movie> GetMoviesFromJson(string fileName);
    }
}
