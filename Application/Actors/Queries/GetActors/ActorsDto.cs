using MyMovieLibrary.Application.Common.Mappings;
using MyMovieLibrary.Domain.Entities;

namespace MyMovieLibrary.Application.Actors.Queries.GetActors
{
    public class ActorsDto : IMapFrom<Actor>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
