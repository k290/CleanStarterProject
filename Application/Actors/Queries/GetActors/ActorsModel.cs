﻿using System.Collections.Generic;

namespace MyMovieLibrary.Application.Actors.Queries.GetActors
{
    public class ActorsModel
    {
        public IList<ActorsDto> Actors { get; set; } = new List<ActorsDto>();
        public int Total { get; set; }
    }
}
