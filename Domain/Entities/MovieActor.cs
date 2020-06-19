using System;
using System.Collections.Generic;

namespace MyMovieLibrary.Domain.Entities
{
    public class MovieActor
    {
        public Guid MovieId { get; set; }
        public Guid ActorId { get; set; }
        public Movie Movie { get; set; }
        public Actor Actor { get; set; }

    }
}