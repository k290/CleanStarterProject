using MyMovieLibrary.Domain.Enums;
using System;
using System.Collections.Generic;

namespace MyMovieLibrary.Domain.Entities
{
    public class Movie
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public int Year { get; set; }

        public Location Location { get; set; }
        public Guid DirectorId { get; set; }

        public ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
        public Director Director { get; set; }
    }
}

