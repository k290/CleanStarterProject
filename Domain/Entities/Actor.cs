using System;
using System.Collections.Generic;

namespace MyMovieLibrary.Domain.Entities
{
    public class Actor
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public IList<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
    }
}