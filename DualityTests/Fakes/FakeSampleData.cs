using DualityApplication.Interfaces;
using DualityApplication.Models;

namespace DualityTests.Fakes
{
    public class FakeSampleData : IExternalFunctions
    {
        public int MaxLinksDepth { get; }
        public int LinksPerPage { get; }

        int currentLinksDepth = 0;
        readonly int countPerPage = 1;

        public int ExpectedCount => countPerPage * (1 + MaxLinksDepth * LinksPerPage);

        public FakeSampleData(int maxLinksDepth, int linksPerPage) 
        { 
            this.MaxLinksDepth = maxLinksDepth;
            this.LinksPerPage = linksPerPage;
        }

        

        public int CountWordOccurences(Page page, string word)
        {
            return countPerPage;
        }

        public Page DownloadPage(string url)
        {
            return new Page();
        }

        public HashSet<string> GetLinksInPage(Page page)
        {
            var numOfLinks = MaxLinksDepth > currentLinksDepth ? LinksPerPage : 0;
            HashSet<string> links = createLinksList(numOfLinks);
            currentLinksDepth++;
            return links;
        }

        
        
        private static HashSet<string> createLinksList(int numOfLinks)
        {
            HashSet<string> links = new();
            for (int i = 0; i < numOfLinks; i++)
            {
                links.Add($"link{i}");
            }

            return links;
        }
    }
}
