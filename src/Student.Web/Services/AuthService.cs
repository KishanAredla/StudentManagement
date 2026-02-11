using Student_WebApp.Models;

namespace Student_WebApp.Services
{
    public class AuthService
    {
        private readonly HttpClient _client;

        public AuthService(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> LoginAsync(LoginViewModel model)
        {
            var response = await _client.PostAsJsonAsync("Auth/login", model);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
            return result.Token;
        }
    }

}
