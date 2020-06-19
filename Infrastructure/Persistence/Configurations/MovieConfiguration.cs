using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyMovieLibrary.Domain.Entities;

namespace MyMovieLibrary.Infrastructure.Persistence.Configurations
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.Property(t => t.Id)
              .HasDefaultValueSql("newsequentialid()");

            builder.Property(t => t.DirectorId)
                .IsRequired();

            builder.Property(t => t.Title)
              .IsRequired().HasMaxLength(200);
        }
    }
}
