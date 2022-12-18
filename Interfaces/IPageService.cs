using Duality.Models;

namespace Duality.Interfaces
{
    public interface IPageService
    {
        Page GetFullPageData(string url, string word);
    }
}