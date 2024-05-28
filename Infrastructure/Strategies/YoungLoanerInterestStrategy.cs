using Application.Interfaces;
using Domain.Settings;
using Microsoft.Extensions.Options;

namespace Infrastructure.Strategies;
internal class YoungLoanerInterestStrategy: ILoanInterestStrategy {
    private readonly IOptions<LoanSettings> _settingsOptions;

    public YoungLoanerInterestStrategy(IOptions<LoanSettings> settingsOptions) {
        _settingsOptions = settingsOptions;
    }
    public Task<double> CalculateInterest(double amount, int periodInMonths) {
        var loanSettings = _settingsOptions.Value;
        var interests = loanSettings.Interests;

        var basicInterest = interests.TwoPercent + loanSettings.PrimeInterest;

        var extraMonthsInterest = 0.0;
        if (periodInMonths > loanSettings.PeriodForRegularLoanInMonth) {
            extraMonthsInterest = (periodInMonths - loanSettings.PeriodForRegularLoanInMonth) * interests.FifteenHunderedsPercent;
        }

        var totalInterest = amount * (basicInterest + extraMonthsInterest);
        return Task.FromResult(amount + totalInterest);
    }
}
