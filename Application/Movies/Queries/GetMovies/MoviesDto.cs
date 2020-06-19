using AutoMapper;
using MyMovieLibrary.Application.Common.Mappings;
using MyMovieLibrary.Domain.Entities;
using MyMovieLibrary.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyMovieLibrary.Application.Movies.Queries.GetMovies
{
    public class MoviesDto : IMapFrom<Movie>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public Location Location { get; set; }
        public IList<MovieListActorDto> Actors { get; set; } = new List<MovieListActorDto>();
        public MovieListDirectorDto Director { get; set; }
        public string ActorString
        {
            get
            {
                return string.Join(", ", Actors.Select(x => x.FullName));
            }
        }

        public string DirectorString
        {
            get
            {
                return Director.FullName;
            }
        }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Movie, MoviesDto>()
               .ForMember(d => d.Actors, opt => opt.MapFrom(s => s.MovieActors.Select(ma => ma.Actor)))
               .ForMember(d => d.Director, opt => opt.MapFrom(s => s.Director));


        }
    }
}
