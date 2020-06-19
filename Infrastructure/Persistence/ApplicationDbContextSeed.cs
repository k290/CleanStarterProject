using MyMovieLibrary.Domain.Entities;
using MyMovieLibrary.Domain.Enums;
using MyMovieLibrary.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMovieLibrary.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {


            // Seed, if necessary
            if (!context.Actors.Any())
            {
                context.Actors.Add(new Actor { Name = "George", Surname = "Clooney" });
                context.Actors.Add(new Actor { Name = "Sylvester", Surname = "Stallone" });
                context.Actors.Add(new Actor { Name = "Morgan", Surname = "Freeman" });
                context.Actors.Add(new Actor { Name = "Johnny", Surname = "Depp" });
                context.Actors.Add(new Actor { Name = "Roger", Surname = "Moore" });
                context.Actors.Add(new Actor { Name = "Charlize", Surname = " Theron" });
                context.Actors.Add(new Actor { Name = "Ashton", Surname = "Kutcher" });
                context.Actors.Add(new Actor { Name = "Jackie", Surname = "Chan" });
                context.Actors.Add(new Actor { Name = "Dwayne", Surname = "Johnson" });
                context.Actors.Add(new Actor { Name = "Rowan", Surname = "Atkinson" });
                context.Actors.Add(new Actor { Name = "Halle", Surname = "Berry" });
                context.Actors.Add(new Actor { Name = "Brad", Surname = "Pitt" });
                context.Actors.Add(new Actor { Name = "Matt", Surname = "Damon" });
                context.Actors.Add(new Actor { Name = "Edward", Surname = "Norton" });

                await context.SaveChangesAsync();
            }


            // Seed, if necessary
            if (!context.Directors.Any())
            {
                context.Directors.Add(new Director { Name = "Steven", Surname = "Soderbergh" });
                context.Directors.Add(new Director { Name = "David", Surname = "Fincher" });

                await context.SaveChangesAsync();
            }


            // Seed, if necessary
            if (!context.Movies.Any())
            {
                context.Movies.Add(new Movie
                {
                    Title = "Oceans Eleven",
                    Year = 2011,
                    Location = Location.DVD,
                    Director = context.Directors.FirstOrDefault(x => x.Surname == "Soderbergh"),
                    MovieActors = new List<MovieActor>()
                    {
                       new MovieActor{Actor= context.Actors.FirstOrDefault(x=>x.Surname == "Clooney") },
                       new MovieActor{Actor  = context.Actors.FirstOrDefault(x=>x.Surname == "Pitt") },
                       new MovieActor{Actor  = context.Actors.FirstOrDefault(x=>x.Surname == "Damon") },
                    }
                });

                context.Movies.Add(new Movie
                {
                    Title = "Fight Club",
                    Year = 1999,
                    Location = Location.Download,
                    Director = context.Directors.FirstOrDefault(x => x.Surname == "Fincher"),
                    MovieActors = new List<MovieActor>()
                    {
                       new MovieActor{Actor  = context.Actors.FirstOrDefault(x=>x.Surname == "Pitt") },
                       new MovieActor{Actor  = context.Actors.FirstOrDefault(x=>x.Surname == "Norton") },
                    }
                });

                await context.SaveChangesAsync();
            }
        }
    }
}
