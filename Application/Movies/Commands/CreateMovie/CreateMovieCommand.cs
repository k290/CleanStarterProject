using MediatR;
using MyMovieLibrary.Application.Common.Interfaces;
using MyMovieLibrary.Domain.Entities;
using MyMovieLibrary.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyMovieLibrary.Application.Movies.Commands.CreateMovie
{
    public class CreateMovieCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public Location Location { get; set; }
        public Guid DirectorId { get; set; }
        public IList<Guid> ActorIds { get; set; }
    }

    public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateMovieCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = new Movie
            {
                Title = request.Title,
                Year = request.Year,
                DirectorId = request.DirectorId,
                Location = request.Location
            };

            foreach (var actorId in request.ActorIds)
            {
                var newMovieActor = new MovieActor { ActorId = actorId };
                _context.MovieActors.Attach(newMovieActor);
                movie.MovieActors.Add(newMovieActor);
            }

            _context.Movies.Add(movie);

            await _context.SaveChangesAsync(cancellationToken);

            //todo: could also publish a mediatr event for "things" that need to respond to this update?? Need an example

            return movie.Id;
        }
    }
}
