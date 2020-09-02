using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyMovieLibrary.Application.Actors.Queries.GetLookups;
using MyMovieLibrary.Application.Directors.Queries.GetLookups;
using MyMovieLibrary.Application.Movies.Queries.GetMovies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Presenters
{
    public class GetAllMoviesPresenter : IGetAllMoviesPresenter
    {
        public GetAllMoviesPresenter(IMediator mediator)
        {
            Mediator = mediator;
        }

        public IMediator Mediator { get; }

        public ActionResult<AllMoviesViewModel> GetViewModel(MoviesModel moviesModel, ActorLookupModel actorLookupModel, DirectorLookupModel directorLookupModel)
        {
            return new AllMoviesViewModel
            {
                Movies = moviesModel.Movies,
                Total = moviesModel.Movies.Count,
                ActorLookups = actorLookupModel.ActorLookups,
                DirectorLookups = directorLookupModel.DirectorLookups
            };
        }
    }

    public interface IGetAllMoviesPresenter
    {
        ActionResult<AllMoviesViewModel> GetViewModel(MoviesModel moviesModel, ActorLookupModel actorLookupModel, DirectorLookupModel directorLookupModel);
    }
}
