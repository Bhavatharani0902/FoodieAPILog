public interface IUserService
{
    void CreateUser(User user);
    List<User> GetAllUsers();
    User GetUser(int userID);
    void EditUser(User user); // This method should be declared in the interface
    void DeleteUser(int userID);
    User ValidateUser(string email, string password); // Corrected method name
}
