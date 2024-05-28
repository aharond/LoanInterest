using MediatR;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.User;
public class LoanCommand: IRequest<double> {
    public int Id { get; set; }
    [DefaultValue(1)]
    [Range(1, int.MaxValue, ErrorMessage = "Please enter an amount bigger or equal than {1}")]
    public double Amount { get; set; }
    [DefaultValue(12)]
    [Range(12, int.MaxValue, ErrorMessage = "Please enter a month bigger or equal than {1}")]
    public int PeriodInMonths { get; set; }
}
