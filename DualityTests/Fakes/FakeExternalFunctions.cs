using DualityApplication.Interfaces;
using DualityApplication.Models;

namespace DualityTests.Fakes
{
    public class FakeExternalFunctions : IExternalFunctions
    {
        public int CountWordOccurences(Page page, string word)
        {
            var rand = new Random();
            return rand.Next(0, 10);
        }

        public Page DownloadPage(string url)
        {
            return new Page();
        }

        public HashSet<string> GetLinksInPage(Page page)
        {
            var rand = new Random();
            HashSet<string> links = new();
            for(int i=0; i < rand.Next(0, 10); i++)
            {
                links.Add($"link{i}");
            }
            return links;
        }
    }
}
