using AutoMapper;
using AutoMapper.QueryableExtensions;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyMovieLibrary.Application.Actors.Queries.GetLookups;
using MyMovieLibrary.Application.Common.Interfaces;
using MyMovieLibrary.Application.Directors.Queries.GetLookups;
using MyMovieLibrary.Domain.Entities;
using MyMovieLibrary.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace MyMovieLibrary.Application.Movies.Queries.GetMovies
{
    public class GetMoviesQuery : IRequest<MoviesVm>
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public string Title { get; set; }
        public int? Year { get; set; }
        public IList<Location> Locations { get; set; } = new List<Location>();
        public IList<Guid> DirectorIds { get; set; } = new List<Guid>();
        public IList<Guid> ActorIds { get; set; } = new List<Guid>();
    }

    public class GetMoviesQueryHandler : IRequestHandler<GetMoviesQuery, MoviesVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GetMoviesQueryHandler(IApplicationDbContext context, IMapper mapper, IMediator Mediator)
        {
            _context = context;
            _mapper = mapper;
            _mediator = Mediator;
        }

        public async Task<MoviesVm> Handle(GetMoviesQuery request, CancellationToken cancellationToken)
        {
            var movies = await _context.Movies
                .Include(x => x.Director)
                .Include(x => x.MovieActors).ThenInclude(x => x.Actor)
                .OrderBy(a => a.Title)
                .Skip(request.Skip)
                .Take(request.Take)
                .Where(Filter(request))
                .ProjectTo<MoviesDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var count = await _context.Movies.CountAsync(cancellationToken);

            var actorLookupVm = await _mediator.Send(new GetActorLookupQuery());
            var directorLookupVm = await _mediator.Send(new GetDirectorLookupQuery());

            return new MoviesVm
            {
                Movies = movies,
                ActorLookups = actorLookupVm.ActorLookups,
                DirectorLookups = directorLookupVm.DirectorLookups,
                Total = count,
            };
        }

        public Expression<Func<Movie, bool>> Filter(GetMoviesQuery request)
        {
            Expression<Func<Movie, bool>> expression = c => true;
            Expression<Func<Movie, bool>> title = c => c.Title.ToLower().StartsWith(request.Title.ToLower());
            Expression<Func<Movie, bool>> year = c => c.Year == request.Year;
            Expression<Func<Movie, bool>> location = c => request.Locations.Contains(c.Location);
            Expression<Func<Movie, bool>> director = c => request.DirectorIds.Contains(c.DirectorId);
            Expression<Func<Movie, bool>> actor = c => c.MovieActors.Any(x => request.ActorIds.Contains(x.ActorId));

            if (!string.IsNullOrWhiteSpace(request.Title))
            {
                expression = expression.And(title);
            }

            if (request.Year != null)
            {
                expression = expression.And(year);
            }

            if (request.Locations.Any())
            {
                expression = expression.And(location);
            }


            if (request.DirectorIds.Any())
            {
                expression = expression.And(director);
            }


            if (request.ActorIds.Any())
            {
                expression = expression.And(actor);
            }


            return expression.Expand();
        }
    }
}
