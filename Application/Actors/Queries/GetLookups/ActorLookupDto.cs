﻿using MyMovieLibrary.Application.Common.Mappings;
using MyMovieLibrary.Domain.Entities;
using System;

namespace MyMovieLibrary.Application.Actors.Queries.GetLookups
{
    public class ActorLookupDto : IMapFrom<Actor>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
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
