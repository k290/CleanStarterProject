using MyMovieLibrary.Application.Actors.Queries.GetLookups;
using MyMovieLibrary.Application.Directors.Queries.GetLookups;
using System.Collections.Generic;

namespace MyMovieLibrary.Application.Movies.Queries.GetMovie
{
    public class MovieVm
    {
        public MovieDto Movie { get; set; }
        public IList<ActorLookupDto> ActorLookups { get; set; } = new List<ActorLookupDto>();
        public IList<DirectorLookupDto> DirectorLookups { get; set; } = new List<DirectorLookupDto>();
    }
}