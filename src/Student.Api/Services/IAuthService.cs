using StudentApi.DTOs;

namespace StudentApi.Services
{
    public interface IAuthService
    {
        string GenerateToken(string username, string role);
    }
}
