using AutoMapper;
using MyMovieLibrary.Application.Common.Mappings;
using MyMovieLibrary.Domain.Entities;
using MyMovieLibrary.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyMovieLibrary.Application.Movies.Queries.GetMovie
{
    public class MovieDto: IMapFrom<Movie>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public Location Location { get; set; }
        public IList<MovieDisplayActorDto> Actors { get; set; } = new List<MovieDisplayActorDto>();
        public MovieDisplayDirectorDto Director { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Movie, MovieDto>()
               .ForMember(d => d.Actors, opt => opt.MapFrom(s => s.MovieActors.Select(ma => ma.Actor)))
               .ForMember(d => d.Director, opt => opt.MapFrom(s => s.Director));


        }
    }
}