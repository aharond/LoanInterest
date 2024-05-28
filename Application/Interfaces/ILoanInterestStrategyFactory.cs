namespace Application.Interfaces {
    public interface ILoanInterestStrategyFactory {
        public ILoanInterestStrategy GetStrategy(int age);
    }
}
