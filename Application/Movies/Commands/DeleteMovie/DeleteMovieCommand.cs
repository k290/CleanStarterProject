using MediatR;
using Microsoft.EntityFrameworkCore;
using MyMovieLibrary.Application.Common.Exceptions;
using MyMovieLibrary.Application.Common.Interfaces;
using MyMovieLibrary.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyMovieLibrary.Application.Movies.Commands.DeleteMovie
{
    public class DeleteMovieCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteMovieCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Movies
                .Include(x=>x.MovieActors)
                .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Movie), request.Id);
            }

            foreach(var movieActor in entity.MovieActors)
            {
                _context.MovieActors.Remove(movieActor);
            }

            _context.Movies.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
