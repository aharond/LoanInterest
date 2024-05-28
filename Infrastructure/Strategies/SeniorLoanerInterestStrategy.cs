using Application.Interfaces;
using Domain.Settings;
using Microsoft.Extensions.Options;

namespace Infrastructure.Strategies;
internal class SeniorLoanerInterestStrategy : ILoanInterestStrategy {
    private readonly IOptions<LoanSettings> _settingsOptions;

    public SeniorLoanerInterestStrategy(IOptions<LoanSettings> settingsOptions) {
        _settingsOptions = settingsOptions;
    }

    public Task<double> CalculateInterest(double amount, int periodInMonths) {
        var loanSettings = _settingsOptions.Value;
        var interests = loanSettings.Interests;
        var loanAmounts = loanSettings.LoanAmounts.Senior;

        double basicInterest;

        if (amount <= loanAmounts.MinLoanAmount) {
            basicInterest = interests.OneAndHalfPercent + loanSettings.PrimeInterest;
        } else if (amount > loanAmounts.MinLoanAmount && amount <= loanAmounts.MaxLoanAmount) {
            basicInterest = interests.ThreePercent + loanSettings.PrimeInterest;
        } else {
            basicInterest = interests.OnePercent;
        }

        var extraMonthsInterest = periodInMonths > loanSettings.PeriodForRegularLoanInMonth
            ? (periodInMonths - loanSettings.PeriodForRegularLoanInMonth) * interests.FifteenHunderedsPercent
            : 0;

        var totalInterest = amount * (basicInterest + extraMonthsInterest);

        return Task.FromResult(amount + totalInterest);
    }
}
