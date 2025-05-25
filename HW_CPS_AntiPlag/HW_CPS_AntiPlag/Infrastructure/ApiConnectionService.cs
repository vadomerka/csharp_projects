namespace HW_CPS_AntiPlag.Infrastructure
{
    /// <summary>
    /// Сервис для отправления запросов по url.
    /// </summary>
    public class ApiConnectionService
    { 
        public ApiConnectionService() { }

        public async Task<HttpResponseMessage> GetResponse(HttpClient httpClient, string url) {
            if (string.IsNullOrWhiteSpace(url)) throw new ArgumentNullException(nameof(url));
            return await httpClient.GetAsync(url);
        }

        public async Task<HttpResponseMessage> PostResponse(HttpClient httpClient, string url, HttpContent content)
        {
            if (string.IsNullOrWhiteSpace(url)) throw new ArgumentNullException(nameof(url));
            return await httpClient.PostAsync(url, content);
        }
    }
}
