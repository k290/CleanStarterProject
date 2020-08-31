using Microsoft.AspNetCore.Mvc;
using MyMovieLibrary.Application.Actors.Queries.GetActors;
using MyMovieLibrary.Application.Actors.Queries.GetLookups;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class ActorsController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<ActorsModel>> Get([FromQuery]GetActorsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<ActorLookupModel>> GetLookups()
        {
            return await Mediator.Send(new GetActorLookupQuery());
        }
    }
}
