using FileAnaliseService.Models;

namespace FileAnaliseService.Infrastructure
{
    /// <summary>
    /// Сервис для получения содержания файла по id.
    /// </summary>
    public class FileContentsService
    {
        private readonly string _root = "";
        public FileContentsService(string root = "https://localhost:7147") { _root = root; }

        public async Task<FileContents> GetFileContents(HttpClient httpClient, int id) {
            var res = new FileContents();

            string uri = _root + $"/file/{id}";
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                res.Contents = responseBody;
                res.FileId = id;
                return res;
            }
            catch (Exception) {
                throw new HttpRequestException();
            }
        }
    }
}
