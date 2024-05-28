using Application.Interfaces;
using Domain.Settings;
using Microsoft.Extensions.Options;

namespace Infrastructure.Strategies;
internal class AdultLoanerInterestStrategy: ILoanInterestStrategy {
    private readonly IOptions<LoanSettings> _settingsOptions;

    public AdultLoanerInterestStrategy(IOptions<LoanSettings> settingsOptions) {
        _settingsOptions = settingsOptions;
    }

    public Task<double> CalculateInterest(double amount, int periodInMonths) {
        double basicInterest;
        var loanSettings = _settingsOptions.Value;
        var interests = loanSettings.Interests;
        var loanAmounts = loanSettings.LoanAmounts.Adult;

        if (amount <= loanAmounts.MinLoanAmount) {
            basicInterest = interests.TwoPercent;
        } else if (amount > loanAmounts.MinLoanAmount && amount <= loanAmounts.MaxLoanAmount) {
            basicInterest = interests.OneAndHalfPercent + loanSettings.PrimeInterest;
        } else {
            basicInterest = interests.OnePercent + loanSettings.PrimeInterest;
        }

        double extraMonthsInterest = 0.0;
        if (periodInMonths > loanSettings.PeriodForRegularLoanInMonth) {
            extraMonthsInterest = (periodInMonths - loanSettings.PeriodForRegularLoanInMonth) * interests.FifteenHunderedsPercent;
        }

        var totalInterest = amount * (basicInterest + extraMonthsInterest);
        var totalAmount = amount + totalInterest;

        return Task.FromResult(totalAmount);
    }
}
