﻿using Duality.Interfaces;
using Duality.Models;

namespace Duality.ExternalFunctions
{
    public class ExternalFunctions : IExternalFunctions
    {

        public Page DownloadPage(string url)
        {
            throw new NotImplementedException();
        }

        public HashSet<string> GetLinksInPage(Page page)
        {
            throw new NotImplementedException();
        }

        public int CountWordOccurences(Page page, string word)
        {
            throw new NotImplementedException();
        }
    }
}
