using System;

namespace MyMovieLibrary.Domain.Entities
{
    public class Director
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}