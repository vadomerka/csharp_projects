using System.Security.Cryptography;
using System.Text;

namespace FilesStoringService.Services
{
    public class FileToHashService
    {
        public FileToHashService() { }

        public static string MD5Hash(string input) {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(result).Replace("-", "").ToLowerInvariant();
            }
        }

        // Используется для проверки файла на пустоту.
        public static string GetEmptyHash() { return MD5Hash(""); }

        public string GetFileContents(IFormFile file) {
            var result = new StringBuilder();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    result.AppendLine(reader.ReadLine());
            }
            return result.ToString();
        }

        public string GetHash(IFormFile file) {
            return MD5Hash(GetFileContents(file));
        }
    }
}
