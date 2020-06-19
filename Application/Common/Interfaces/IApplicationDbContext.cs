using Microsoft.EntityFrameworkCore;
using MyMovieLibrary.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MyMovieLibrary.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Movie> Movies { get; set; }
        DbSet<Actor> Actors { get; set; }
        DbSet<MovieActor> MovieActors { get; set; }
        DbSet<Director> Directors { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
