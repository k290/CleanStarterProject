using MyMovieLibrary.Application.Actors.Queries.GetActors;
using System.Collections.Generic;

namespace MyMovieLibrary.Application.Actors.Queries.GetLookups
{
    public class ActorLookupVm
    {
        public IList<ActorLookupDto> ActorLookups { get; set; }
    }
}
