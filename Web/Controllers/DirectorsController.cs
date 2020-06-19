using Microsoft.AspNetCore.Mvc;
using MyMovieLibrary.Application.Directors.Queries.GetLookups;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class DirectorsController : ApiController
    {
        [HttpGet("[action]")]
        public async Task<ActionResult<DirectorLookupVm>> GetLookups()
        {
            return await Mediator.Send(new GetDirectorLookupQuery());
        }
    }
}
