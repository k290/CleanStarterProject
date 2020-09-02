using Microsoft.AspNetCore.Mvc;
using MyMovieLibrary.Application.Actors.Queries.GetLookups;
using MyMovieLibrary.Application.Directors.Queries.GetLookups;
using MyMovieLibrary.Application.Movies.Commands.CreateMovie;
using MyMovieLibrary.Application.Movies.Commands.DeleteMovie;
using MyMovieLibrary.Application.Movies.Commands.UpdateMovie;
using MyMovieLibrary.Application.Movies.Queries.GetMovie;
using MyMovieLibrary.Application.Movies.Queries.GetMovies;
using System;
using System.Threading.Tasks;
using Web.Presenters;

namespace Web.Controllers
{
    public class MoviesController : ApiController
    {
        private readonly IGetAllMoviesPresenter getAllMoviesPresenter;

        public MoviesController(IGetAllMoviesPresenter getAllMoviesPresenter)
        {
            this.getAllMoviesPresenter = getAllMoviesPresenter;
        }


        [HttpGet]
        public async Task<ActionResult<MovieModel>> Get(Guid id)
        {
            return await Mediator.Send(new GetMovieQuery { Id = id });
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<AllMoviesViewModel>> GetAll([FromQuery] GetMoviesQuery query)
        {
            var moviesModel = await Mediator.Send(query);
            var actorLookupModel = await Mediator.Send(new GetActorLookupQuery());
            var directorLookupModel = await Mediator.Send(new GetDirectorLookupQuery());
            return getAllMoviesPresenter.GetViewModel(moviesModel, actorLookupModel, directorLookupModel);
        }

        [HttpPut("{id}")] //todo think about why its better to also specify the id as part of the URL :? RESTful?
        public async Task<ActionResult> Update(Guid id, UpdateMovieCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(CreateMovieCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await Mediator.Send(new DeleteMovieCommand { Id = id });

            return NoContent();
        }
    }
}
