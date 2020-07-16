using AutoMapper;
using AutoMapper.QueryableExtensions;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyMovieLibrary.Application.Common.Interfaces;
using MyMovieLibrary.Domain.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace MyMovieLibrary.Application.Actors.Queries.GetActors
{
    public class GetActorsQuery : IRequest<ActorsModel>
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }

    public class GetActorsQueryHandler : IRequestHandler<GetActorsQuery, ActorsModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetActorsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActorsModel> Handle(GetActorsQuery request, CancellationToken cancellationToken)
        {
            var actors = await _context.Actors
                .OrderBy(a => a.Name).ThenBy(a => a.Surname)
                .Skip(request.Skip)
                .Take(request.Take)
                .Where(Filter(request))
                .ProjectTo<ActorsDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var count = await _context.Actors.CountAsync(cancellationToken);

            return new ActorsModel
            {
                Actors = actors,
                Total = count
            };
        }

        public Expression<Func<Actor, bool>> Filter(GetActorsQuery request)
        {
            Expression<Func<Actor, bool>> expression = c => true;
            Expression<Func<Actor, bool>> name = c => c.Name.ToLower().StartsWith(request.Name.ToLower());
            Expression<Func<Actor, bool>> surname = c => c.Surname.ToLower().StartsWith(request.Surname.ToLower());

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                expression = expression.And(name);
            }

            if (!string.IsNullOrWhiteSpace(request.Surname))
            {
                expression = expression.And(surname);
            }
            return expression.Expand();
        }
    }
}
