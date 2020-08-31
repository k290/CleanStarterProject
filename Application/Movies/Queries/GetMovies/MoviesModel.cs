using MyMovieLibrary.Application.Actors.Queries.GetLookups;
using MyMovieLibrary.Application.Directors.Queries.GetLookups;
using System.Collections.Generic;

namespace MyMovieLibrary.Application.Movies.Queries.GetMovies
{
    public class MoviesModel
    {
        public IList<MoviesDto> Movies { get; set; } = new List<MoviesDto>();

    }
}
