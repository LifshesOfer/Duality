namespace DualityApplication.Models
{
    public class Page
    {
        public int WordCount { get; set; }
        public HashSet<string> Links { get; set; } = new();
    }
}
