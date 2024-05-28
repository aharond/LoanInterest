namespace Application.Interfaces {
    public interface ILoanInterestStrategy {
        Task<double> CalculateInterest(double amount, int periodInMonths);
    }
}
