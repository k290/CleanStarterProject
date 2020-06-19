﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyMovieLibrary.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyMovieLibrary.Application.Directors.Queries.GetLookups
{
    public class GetDirectorLookupQuery : IRequest<DirectorLookupVm>
    {

    }

    public class GetDirectorLookupQueryHandler : IRequestHandler<GetDirectorLookupQuery, DirectorLookupVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetDirectorLookupQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DirectorLookupVm> Handle(GetDirectorLookupQuery request, CancellationToken cancellationToken)
        {
            var lookups = await _context.Directors
                .OrderBy(a => a.Name).ThenBy(a => a.Surname)
                .ProjectTo<DirectorLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);


            return new DirectorLookupVm
            {
                DirectorLookups = lookups
            };
        }
    }
}
