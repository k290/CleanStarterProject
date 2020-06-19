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
    public class GetActorLookupQuery : IRequest<ActorLookupVm>
    {

    }

    public class GetActorLookupQueryHandler : IRequestHandler<GetActorLookupQuery, ActorLookupVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetActorLookupQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActorLookupVm> Handle(GetActorLookupQuery request, CancellationToken cancellationToken)
        {
            var lookups = await _context.Actors
                .OrderBy(a => a.Name).ThenBy(a => a.Surname)
                .ProjectTo<ActorLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);


            return new ActorLookupVm
            {
                ActorLookups = lookups
            };
        }
    }
}
