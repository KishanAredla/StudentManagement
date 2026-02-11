namespace StudentApi.Common
{
    public class ApiErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string? Detail { get; set; }   // optional, for debugging
    }
}
