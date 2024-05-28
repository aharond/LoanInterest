using Application.Interfaces;
using Domain;
using Newtonsoft.Json;

namespace Infrastructure.Services;
public class UserRepository: IUserRepository {
    private IList<User> _users = [];
    private readonly string _usersDataPath = Path.Combine(Directory.GetCurrentDirectory(), "Users.json");

    public UserRepository() => LoadUsers();
    private void LoadUsers() {
        try {
            using FileStream stream = File.OpenRead(_usersDataPath);
            var serializer = new JsonSerializer();

            using var sr = new StreamReader(stream);
            using var jsonTextReader = new JsonTextReader(sr);
            _users = serializer.Deserialize<User[]>(jsonTextReader);
        } catch (Exception) {
            throw;
        }
    }
    public User GetUserById(int id) {
        return _users.FirstOrDefault(c => c.Id == id);
    }
}
