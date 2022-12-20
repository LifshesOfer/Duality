using DualityApplication.Models;

namespace DualityApplication.Interfaces
{
    public interface IExternalFunctions
    {
        public int CountWordOccurences(Page page, string word);
        public Page DownloadPage(string url);
        public HashSet<string> GetLinksInPage(Page page);
    }
}