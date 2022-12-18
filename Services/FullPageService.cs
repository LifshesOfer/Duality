using Duality.Interfaces;
using Duality.Models;

namespace Duality.Services
{
    public class FullPageService : IExternalFunctions, IPageService
    {
        private readonly IExternalFunctions externalFunctions;

        public FullPageService(IExternalFunctions externalFunctions)
        {
            this.externalFunctions = externalFunctions;
        }

        public Page GetFullPageData(string url, string word)
        {
            var page = DownloadPage(url);
            page.WordCount = CountWordOccurences(page, word);
            page.Links = GetLinksInPage(page);
            return page;
        }

        public Page DownloadPage(string url)
        {
            try
            {
                return externalFunctions.DownloadPage(url);
            }
            catch (Exception ex)
            {
                var message = $"Failed to download page from {url}";
                throw new Exception(message, ex);
            }
        }

        public int CountWordOccurences(Page page, string word)
        {
            try
            {
                return externalFunctions.CountWordOccurences(page, word);
            }
            catch (Exception ex)
            {
                var message = "Failed to count word occurences";
                throw new Exception(message, ex);
            }
        }



        public HashSet<string> GetLinksInPage(Page page)
        {
            try
            {
                return externalFunctions.GetLinksInPage(page);
            }
            catch (Exception ex)
            {
                var message = "Failed to get list of links from page";
                throw new Exception(message, ex);
            }
        }
    }
}
