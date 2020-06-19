using MediatR;
using Microsoft.EntityFrameworkCore;
using MyMovieLibrary.Application.Common.Exceptions;
using MyMovieLibrary.Application.Common.Interfaces;
using MyMovieLibrary.Domain.Entities;
using MyMovieLibrary.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyMovieLibrary.Application.Movies.Commands.UpdateMovie
{
    public class UpdateMovieCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public Location Location { get; set; }
        public Guid DirectorId { get; set; }
        public IList<Guid> ActorIds { get; set; }
    }

    public class UpdateMovieCommandHandler : IRequestHandler<UpdateMovieCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateMovieCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = await _context.Movies
                .Include(x=>x.MovieActors)
                .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (movie == null)
            {
                throw new NotFoundException(nameof(Movie), request.Id);
            }

            movie.Title = request.Title;
            movie.Year = request.Year;
            movie.DirectorId = request.DirectorId;

            foreach (var movieActor in movie.MovieActors.ToList())
            {
                if (!request.ActorIds.Contains(movieActor.ActorId))
                    movie.MovieActors.Remove(movieActor);
            }

            foreach (var actorId in request.ActorIds)
            {
                if (!movie.MovieActors.Any(r => r.ActorId == actorId))
                {
                    var newMovieActor = new MovieActor { ActorId = actorId };
                    _context.MovieActors.Attach(newMovieActor);
                    movie.MovieActors.Add(newMovieActor);
                }
            }

            await _context.SaveChangesAsync(cancellationToken);

            //todo: could also publish a mediatr event for "things" that need to respond to this update?? Need an example

            return Unit.Value; 
        }
    }
}
