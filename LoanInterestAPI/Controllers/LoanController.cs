using Api.Controllers.Base;
using Application.User;
using Microsoft.AspNetCore.Mvc;

namespace LoanInterestAPI.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class LoanController : ApiControllerBase {
        [HttpPost("loan-calculator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CalculateLoan([FromBody] LoanCommand request) {
            var result = await Mediator.Send(request);
            return Ok(new { calculateLoanAmount = result });
        }
    }
}
