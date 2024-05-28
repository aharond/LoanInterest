namespace Domain.Settings;

public record Interest(double OnePercent, 
    double OneAndHalfPercent,
    double TwoPercent, 
    double ThreePercent, 
    double FifteenHunderedsPercent);

public record LoanAmountRange(int MinLoanAmount, int MaxLoanAmount);
public record LoanAmounts(LoanAmountRange Adult, LoanAmountRange Senior);

public class LoanSettings
{
    public double PrimeInterest { get; set; }
    public int YoungUserAge { get; set; }
    public int AdultUserAge { get; set; }
    public Interest Interests { get; set; }
    public LoanAmounts LoanAmounts { get; set; }
    public int PeriodForRegularLoanInMonth { get; set; }

}
