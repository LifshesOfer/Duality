using DualityApplication.Models;

namespace DualityApplication.Interfaces
{
    public interface IPageService
    {
        Page GetFullPageData(string url, string word);
    }
}