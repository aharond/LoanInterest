using Domain;

namespace Application.Interfaces;
public interface IUserRepository {
    Domain.User GetUserById(int id);
}
