using Microsoft.EntityFrameworkCore;
using MyMovieLibrary.Application.Common.Interfaces;
using MyMovieLibrary.Domain.Entities;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace MyMovieLibrary.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {

        public ApplicationDbContext(
            DbContextOptions options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieActor> MovieActors { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Director> Directors { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);

            builder.Entity<MovieActor>().HasKey(i => new { i.MovieId, i.ActorId });
        }
    }
}
