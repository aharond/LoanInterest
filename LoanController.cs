using System.Net;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace LoanIterestApi.Controllers;

[ApiController]
public class LoanController : ControllerBase {
    private readonly IClientRepository _clientRepository;
    private readonly LoanInterestStrategyFactory _strategyFactory;

    public LoanController(IClientRepository clientRepository) {
        _clientRepository = clientRepository;
        _strategyFactory = new LoanInterestStrategyFactory();
    }

    [HttpPost("calculate")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CalculateLoan([FromBody] LoanRequest loanRequest) {
        var client = _clientRepository.GetClientById(loanRequest.ClientId);
        if (client == null) {
            return NotFound("Client not found");
        }

        var strategy = _strategyFactory.GetStrategy(client.Age);
        var totalAmount = strategy.CalculateInterest(loanRequest.Amount, loanRequest.PeriodInMonths);
        return Ok(totalAmount);
    }
}