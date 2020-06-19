﻿using Microsoft.AspNetCore.Mvc;
using MyMovieLibrary.Application.Movies.Commands.CreateMovie;
using MyMovieLibrary.Application.Movies.Commands.DeleteMovie;
using MyMovieLibrary.Application.Movies.Commands.UpdateMovie;
using MyMovieLibrary.Application.Movies.Queries.GetMovie;
using MyMovieLibrary.Application.Movies.Queries.GetMovies;
using System;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class MoviesController : ApiController
    {

        [HttpGet]
        public async Task<ActionResult<MovieVm>> Get(Guid id)
        {
            return await Mediator.Send(new GetMovieQuery { Id = id });
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<MoviesVm>> GetAll([FromQuery] GetMoviesQuery query)
        {
            return await Mediator.Send(query);
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