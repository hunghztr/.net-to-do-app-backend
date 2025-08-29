using ToDoList.Models;

namespace ToDoList.Interfaces
{
    public interface ITokenRepository
    {
        string GenerateToken(User user,string typeToken);
        string CheckToken(string token);
    }
}
