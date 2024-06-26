using Application.Interfaces;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection {
    public static IServiceCollection AddInfrastructure(this IServiceCollection services) {
        
        services.AddSingleton<IUserRepository, UserRepository>();
        services.AddSingleton<ILoanInterestStrategyFactory, LoanInterestStrategyFactory>();

        return services;
    }
}
