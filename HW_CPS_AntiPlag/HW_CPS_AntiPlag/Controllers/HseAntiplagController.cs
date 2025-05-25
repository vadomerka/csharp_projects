using Microsoft.AspNetCore.Mvc;

namespace HW_CPS_AntiPlag.Controllers
{
    /// <summary>
    /// Класс наследуемый контроллерами роутера.
    /// </summary>
    public class HseAntiplagController : Controller
    {
        /// <summary>
        /// Метод возвращает содержание http ответа.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        protected async Task<IActionResult> Proxy(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsByteArrayAsync();
            var contentType = response.Content.Headers.ContentType?.ToString() ?? "application/octet-stream";

            if (!response.IsSuccessStatusCode)
                return new ContentResult
                {
                    StatusCode = (int)response.StatusCode,
                    Content = System.Text.Encoding.UTF8.GetString(content),
                    ContentType = contentType
                };

            return new FileContentResult(content, contentType);
        }
    }
}
