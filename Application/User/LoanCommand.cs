using MediatR;

namespace Application.User;
public class LoanCommand: IRequest<double> {
    public int Id { get; set; }
    public double Amount { get; set; }
    public int PeriodInMonths { get; set; }
}
