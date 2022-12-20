using DualityApplication.Interfaces;
using DualityApplication.Models;
using System;

namespace DualityApplication.Services
{
    public class WordCountService
    {
        /// <summary>
        /// Maximum number of pages that can be downloaded. Default is 100 pages.
        /// </summary>
        public int MaxPageDownloads { get; init; } = 100;

        private int pageCounter = 0;
        private readonly IPageService pageService;

        public WordCountService(IExternalFunctions externalFunctions)
        {
            this.pageService = new FullPageService(externalFunctions);
        }

        public int CalculateWordScore(WordScoreConfiguration scoreConfiguration)
        {
            var url = scoreConfiguration.Url;
            var word = scoreConfiguration.Word;

            Page page = pageService.GetFullPageData(url, word);
            var pageTree = new TreeNode<Page>(page);


            PopulatePageTree(pageTree, word);
            var totalWordCount = SumWordCount(pageTree);
            return totalWordCount;
        }

        private static int SumWordCount(TreeNode<Page> pageTree)
        {
            int traverseCount = 0;
            pageTree.Traverse((page) => traverseCount += page.WordCount);
            return traverseCount;
        }

        private void PopulatePageTree(TreeNode<Page> pageTree, string word)
        {
            Interlocked.Increment(ref pageCounter);
            
            if (pageCounter > MaxPageDownloads)
            {
                throw new Exception($"Reached max amount of downloaded pages");
            }

            foreach (var link in pageTree.Value.Links)
            {
                var page = pageService.GetFullPageData(link, word);
                PopulatePageTree(pageTree.AddChild(page), word);
            }
        }
    }
}
