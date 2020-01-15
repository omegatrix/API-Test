using NUnit.Framework;
using System.Net;

namespace API_Test.UnitTests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        [TestCase("https://localhost:44391/WithinLondon", ExpectedResult = HttpStatusCode.OK)]
        [TestCase("https://localhost:44391/AllUsers", ExpectedResult = HttpStatusCode.OK)]
        [TestCase("https://localhost:44391/User/5", ExpectedResult = HttpStatusCode.OK)]
        [TestCase("https://localhost:44391/Users/London", ExpectedResult = HttpStatusCode.OK)]
        public HttpStatusCode API_WithinLondonRequest_ReturnsValidStatusCode(string path)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(path);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            return response.StatusCode;
        }
    }
}