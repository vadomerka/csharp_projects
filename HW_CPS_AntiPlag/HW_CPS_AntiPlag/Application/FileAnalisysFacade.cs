using HW_CPS_AntiPlag.Infrastructure;

namespace HW_CPS_AntiPlag.Application
{
    public class FileAnalisysFacade
    {
        private readonly string _root = "";
        public FileAnalisysFacade(string root = "https://localhost:7051") { _root = root; }

        public async Task<HttpResponseMessage> GetStatResult(int id)
        {
            var acs = new ApiConnectionService();
            string url = _root + $"/statistics/{id}";
            return await acs.GetResponse(new HttpClient(), url);
        }

        public async Task<HttpResponseMessage> GetCompareResult(int id1, int id2)
        {
            var acs = new ApiConnectionService();
            string url = _root + $"/compare/{id1}/{id2}";
            return await acs.GetResponse(new HttpClient(), url);
        }
    }
}
