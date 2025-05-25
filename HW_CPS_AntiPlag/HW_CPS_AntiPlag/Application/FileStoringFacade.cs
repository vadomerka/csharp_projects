using HW_CPS_AntiPlag.Infrastructure;
using HW_CPS_AntiPlag.Models;
using Microsoft.AspNetCore.Mvc;

namespace HW_CPS_AntiPlag.Application
{
    public class FileStoringFacade
    {
        private readonly string _root = "";
        public FileStoringFacade(string root = "https://localhost:7147") { _root = root; }

        public async Task<HttpResponseMessage> GetFiles() {
            var acs = new ApiConnectionService();
            string url = _root + $"/files";
            return await acs.GetResponse(new HttpClient(), url);
        }

        public async Task<HttpResponseMessage> GetFile(int id)
        {
            var acs = new ApiConnectionService();
            string url = _root + $"/file/{id}";
            return await acs.GetResponse(new HttpClient(), url);
        }

        public async Task<HttpResponseMessage> PostFile(FileDTO dto)
        {
            var acs = new ApiConnectionService();
            string url = _root + $"/file";

            // Обертка данных о файле для запроса.
            var content = new MultipartFormDataContent
            {
                { new StringContent(dto.AuthorId.ToString()), "AuthorId" }
            };

            if (dto.File != null && dto.File.Length > 0)
            {
                var fileStream = dto.File.OpenReadStream();
                var streamContent = new StreamContent(fileStream);
                streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(dto.File.ContentType);
                content.Add(streamContent, "File", dto.File.FileName);
            }

            return await acs.PostResponse(new HttpClient(), url, content);
        }
    }
}
