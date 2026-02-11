using System.Net.Http.Headers;

namespace Student_WebApp.Services
{
    public class ApiService
    {
        private readonly HttpClient _client;
        private readonly IHttpContextAccessor _context;

        public ApiService(HttpClient client, IHttpContextAccessor context)
        {
            _client = client;
            _context = context;
        }

        private void AttachToken()
        {
            var token = _context.HttpContext.Session.GetString("JWT");

            if (!string.IsNullOrEmpty(token))
            {
                _client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
        }

        private async Task<HttpResponseMessage> SendAsync(
            Func<Task<HttpResponseMessage>> action)
        {
            var response = await action();

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                _context.HttpContext.Session.Clear();
                _context.HttpContext.Response.Redirect("/Account/Login");
            }

            response.EnsureSuccessStatusCode();
            return response;
        }


        public async Task<T> GetAsync<T>(string url)
        {
            AttachToken();

            var response = await SendAsync(() => _client.GetAsync(url));
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<T> PostAsync<T>(string url, object data)
        {
            AttachToken();

            var response = await SendAsync(() => _client.PostAsJsonAsync(url, data));
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task PutAsync(string url, object data)
        {
            AttachToken();

            var response = await SendAsync(() => _client.PutAsJsonAsync(url, data));
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(string url)
        {
            AttachToken();

            var response = await SendAsync(() => _client.DeleteAsync(url));
            response.EnsureSuccessStatusCode();
        }

        public async Task<T> GetByIdAsync<T>(string url)
        {
            AttachToken();

            var response = await SendAsync(() => _client.GetAsync(url));
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<T>();
        }
    }

}
