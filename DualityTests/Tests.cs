using DualityApplication.Services;
using DualityTests.Fakes;

namespace DualityTests
{
    public class Tests
    {
        public static IEnumerable<TestCaseData> TestCases()
        {
            FakeSampleData data;
            yield return new TestCaseData(data = new FakeSampleData(0, 1)).SetName($"Link Depth {data.MaxLinksDepth} with {data.LinksPerPage} words per page");
            yield return new TestCaseData(data = new FakeSampleData(3, 10)).SetName($"Link Depth {data.MaxLinksDepth} with {data.LinksPerPage} words per page");
            yield return new TestCaseData(data = new FakeSampleData(3, 0)).SetName($"Link Depth {data.MaxLinksDepth} with {data.LinksPerPage} words per page");
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void AcceptanceTest(FakeSampleData mockExtFunctions)
        {

            var countService = new WordCountService(mockExtFunctions);

            var result = countService.CalculateWordScore(new("1", "word"));

            Assert.That(result, Is.EqualTo(mockExtFunctions.ExpectedCount));
        }

        [Test]
        public void TestTooManyPagesToDownload()
        {
            var mockExtFunctions = new FakeSampleData(110, 1);

            var countService = new WordCountService(mockExtFunctions);

            Assert.Throws<Exception>(() => countService.CalculateWordScore(new("1", "word")));
        }

        [Test]
        public void TestMaxDownloadPagesLimit()
        {
            var mockExtFunctions = new FakeSampleData(1, 1);

            var countService = new WordCountService(mockExtFunctions) { MaxPageDownloads = 0};

            Assert.Throws<Exception>(() => countService.CalculateWordScore(new("1", "word")));
        }
    }
}