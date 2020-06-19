using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyMovieLibrary.Application.Actors.Queries.GetLookups;
using MyMovieLibrary.Application.Common.Interfaces;
using MyMovieLibrary.Application.Directors.Queries.GetLookups;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyMovieLibrary.Application.Movies.Queries.GetMovie
{
    public class GetMovieQuery : IRequest<MovieVm>
    {
        public Guid Id { get; set; }

    }

    public class GetMovieQueryHandler : IRequestHandler<GetMovieQuery, MovieVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GetMovieQueryHandler(IApplicationDbContext context, IMapper mapper, IMediator Mediator)
        {
            _context = context;
            _mapper = mapper;
            _mediator = Mediator;
        }

        public async Task<MovieVm> Handle(GetMovieQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Movies
                .Include(x => x.Director)
                .Include(x => x.MovieActors).ThenInclude(x => x.Actor)
                .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            var movie = _mapper.Map<MovieDto>(entity);


            var actorLookupVm = await _mediator.Send(new GetActorLookupQuery());
            var directorLookupVm = await _mediator.Send(new GetDirectorLookupQuery());


            return new MovieVm
            {
                Movie = movie,
                ActorLookups = actorLookupVm.ActorLookups,
                DirectorLookups = directorLookupVm.DirectorLookups
            };
        }

    }
}
