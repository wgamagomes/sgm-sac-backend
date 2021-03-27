using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Polly.CircuitBreaker;
using SGM.SAC.Api.Constants;
using SGM.SAC.Api.Filters;
using SGM.SAC.Api.Models;
using SGM.SAC.Domain.Extensions;
using SGM.SAC.Domain.QuerySide.Queries;
using System.Threading.Tasks;

namespace SGM.SAC.Api.Controllers
{
    [ApiController]
    //[CustomAuthorize("admin", "citizen")]
    [Route("[controller]")]
    public class CitizenServiceController : ControllerBase
    {
        private readonly ILogger<CitizenServiceController> _logger;
        private readonly IMediator _mediator;

        public CitizenServiceController(ILogger<CitizenServiceController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        [Route(Routes.PropertyTaxRoute)]
        public async Task<ActionResult> GetPropertyTax([FromBody] PropertyTaxRequest request)
        {
            try
            {
                var result = await _mediator.Send(PropertyTaxQuery.Create(request.PropertyRegistration, (bool)request.IsRuralTax));

                if (result == null)
                    return NotFound("Property tax was not found.");

                return Ok(result);
            }
            catch (BrokenCircuitException)
            {
                return Problem("Service is inoperative, please try later on.", statusCode: 500);
            }
        }
    }
}
