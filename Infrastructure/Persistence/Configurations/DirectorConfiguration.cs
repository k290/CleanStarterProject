﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyMovieLibrary.Domain.Entities;

namespace MyMovieLibrary.Infrastructure.Persistence.Configurations
{
    public class DirectorConfiguration : IEntityTypeConfiguration<Director>
    {
        public void Configure(EntityTypeBuilder<Director> builder)
        {
            builder.Property(t => t.Id)
                .HasDefaultValueSql("newsequentialid()");

            builder.Property(t => t.Name)
                .IsRequired().HasMaxLength(200);

            builder.Property(t => t.Surname)
              .IsRequired().HasMaxLength(200);
        }
    }
}
