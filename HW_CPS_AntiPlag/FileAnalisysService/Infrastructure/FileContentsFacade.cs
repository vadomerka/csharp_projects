using FileAnalisysService.Models;
using System.Net;
using System;

namespace FileAnalisysService.Infrastructure
{
    public class FileContentsFacade
    {
        public FileContentsFacade() { }

        public async Task<FileContents> GetFileContents(int id) {
            var res = new FileContents();

            string uri = $"https://localhost:7147/file/{id}";
            using HttpClient httpClient = new HttpClient();
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
