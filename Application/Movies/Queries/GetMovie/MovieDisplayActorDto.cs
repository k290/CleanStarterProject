﻿using MyMovieLibrary.Application.Common.Mappings;
using MyMovieLibrary.Domain.Entities;
using System;

namespace MyMovieLibrary.Application.Movies.Queries.GetMovie
{
    public class MovieDisplayActorDto : IMapFrom<Actor>
    {
        public Guid Id { get; set; }

        public string Name { get; set;  }
        public string Surname { get; set; }

        public string FullName
        {
            get
            {
                return $"{Name} {Surname}";
            }
        }


    }
}
