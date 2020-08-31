using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyMovieLibrary.Application.Actors.Queries.GetLookups;
using MyMovieLibrary.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyMovieLibrary.Application.Actors.Queries.GetLookups
{
    public class GetActorLookupQuery : IRequest<ActorLookupModel>
    {

    }

    public class GetActorLookupQueryHandler : IRequestHandler<GetActorLookupQuery, ActorLookupModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetActorLookupQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActorLookupModel> Handle(GetActorLookupQuery request, CancellationToken cancellationToken)
        {
            var lookups = await _context.Actors
                .OrderBy(a => a.Name).ThenBy(a => a.Surname)
                .ProjectTo<ActorLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);


            return new ActorLookupModel
            {
                ActorLookups = lookups
            };
        }
    }
}
