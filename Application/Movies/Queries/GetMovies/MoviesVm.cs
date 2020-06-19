using MyMovieLibrary.Application.Actors.Queries.GetLookups;
using MyMovieLibrary.Application.Directors.Queries.GetLookups;
using System.Collections.Generic;

namespace MyMovieLibrary.Application.Movies.Queries.GetMovies
{
    public class MoviesVm
    {
        public IList<MoviesDto> Movies { get; set; } = new List<MoviesDto>();
        public IList<ActorLookupDto> ActorLookups { get; set; } = new List<ActorLookupDto>();
        public IList<DirectorLookupDto> DirectorLookups { get; set; } = new List<DirectorLookupDto>();
        public int Total { get; set; }
    }
}
