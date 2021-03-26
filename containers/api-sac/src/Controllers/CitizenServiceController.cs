using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SGM.SAC.Api.Filters;
using SGM.SAC.Domain.QuerySide.Queries;
using System.Threading.Tasks;

namespace SGM.SAC.Api.Controllers
{
    [ApiController]
    [CustomAuthorize("admin", "citizen")]
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

        [HttpGet]
        [Route("getPropertyTax")]
        public async Task<ActionResult> GetPropertyTax(string propertyRegistration)
        {
            if (string.IsNullOrEmpty(propertyRegistration))
                return BadRequest("Invalid Property Registration.");

            var result = await _mediator.Send(PropertyTaxQuery.Create(propertyRegistration));

            if (result == null)
                return NotFound("Gantt Project was not found.");

            return Ok(result);
        }
    }
}
