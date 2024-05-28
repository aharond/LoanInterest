using Application.Interfaces;
using MediatR;

namespace Application.User;

internal sealed class GetLoanAmountCommand : IRequestHandler<LoanCommand, double> {
    private readonly ILoanInterestStrategyFactory _strategyFactory;
    private readonly IUserRepository _userRepository;

    public GetLoanAmountCommand(
        IUserRepository userRepository,
        ILoanInterestStrategyFactory strategyFactory) {
        _userRepository = userRepository;
        _strategyFactory = strategyFactory;
    }
    public async Task<double> Handle(LoanCommand request, CancellationToken cancellationToken) {
        var user = _userRepository.GetUserById(request.Id) ?? throw new KeyNotFoundException("User not found");
        var strategy = _strategyFactory.GetStrategy(user.Age);
        var totalAmount = await strategy.CalculateInterest(request.Amount, request.PeriodInMonths);

        return await Task.FromResult(totalAmount);
    }
}