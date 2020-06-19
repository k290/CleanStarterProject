using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MyMovieLibrary.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyMovieLibrary.Application.Movies.Commands.UpdateMovie
{
    public class UpdateMovieCommandValidator : AbstractValidator<UpdateMovieCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateMovieCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.")
                .MustAsync(BeUniqueTitle).WithMessage("The specified title already exists.");

            RuleFor(v => v.ActorIds).NotEmpty().WithMessage("Actors are required");

            RuleFor(v => v.Location).NotEmpty().WithMessage("Location is required");
            RuleFor(v => v.Year).NotEmpty().WithMessage("Year is required");
            RuleFor(v => v.DirectorId).NotEmpty().WithMessage("Director is required");
        }

        public async Task<bool> BeUniqueTitle(UpdateMovieCommand model, string title, CancellationToken cancellationToken)
        {
            return await _context.Movies
                .Where(x => x.Id != model.Id)
                .AllAsync(x => x.Title != title);
        }
    }
}
