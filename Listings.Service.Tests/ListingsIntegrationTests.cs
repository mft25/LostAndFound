using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Flurl;
using Flurl.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Listings.Service.Tests
{
    [TestClass]
    public class ListingsIntegrationTests
    {
        private readonly string _serviceUrl;

        public ListingsIntegrationTests()
        {
            _serviceUrl = ConfigurationManager.AppSettings["ServiceUrl"];
        }

        [TestMethod]
        public void GetAll_Succeeds()
        {
            var listings = _serviceUrl
                .AppendPathSegments("api", "listings")
                .GetJsonAsync<IList<Listing>>()
                .Result;

            Assert.IsNotNull(listings);
            Assert.IsTrue(listings.Any());
        }
    }
}
