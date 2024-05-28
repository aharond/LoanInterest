using Application.Interfaces;
using Domain.Settings;
using Infrastructure.Strategies;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services;
public class LoanInterestStrategyFactory : ILoanInterestStrategyFactory {
    private readonly IOptions<LoanSettings> _settingsOptions;

    public LoanInterestStrategyFactory(IOptions<LoanSettings> settingsOptions) {
        _settingsOptions = settingsOptions;
    }

    public ILoanInterestStrategy GetStrategy(int age) {
        ILoanInterestStrategy strategy;

        if (age < _settingsOptions.Value.YoungUserAge) {
            strategy = new YoungLoanerInterestStrategy(_settingsOptions);
        } else if (age >= _settingsOptions.Value.YoungUserAge && age <= _settingsOptions.Value.AdultUserAge) {
            strategy = new AdultLoanerInterestStrategy(_settingsOptions);
        } else {
            strategy = new SeniorLoanerInterestStrategy(_settingsOptions);
        }

        return strategy;
    }
}
