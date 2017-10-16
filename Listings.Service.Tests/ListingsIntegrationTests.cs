using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
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
            _serviceUrl = ConfigurationManager.AppSettings["ServiceUrl"] + "/api/listings";
        }

        [TestMethod]
        public void GetAll_Succeeds()
        {
            var listings = _serviceUrl
                .GetJsonAsync<IList<Listing>>()
                .Result;

            Assert.IsNotNull(listings);
            Assert.IsTrue(listings.Any());
        }

        [TestMethod]
        public void Get_Succeeds()
        {
            var listing = _serviceUrl
                .AppendPathSegments("1")
                .GetJsonAsync<Listing>()
                .Result;

            Assert.AreEqual("Test1", listing.Text);
        }

        [TestMethod]
        public void Put_Succeeds()
        {
            var listing = new Listing
            {
                Text = "Test2"
            };

            var response = _serviceUrl
                .AppendPathSegments("2")
                .PutJsonAsync(listing)
                .Result;

            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [TestMethod]
        public void PostDelete_Succeeds()
        {
            var listing = new Listing
            {
                Text = "Test"
            };

            var id = _serviceUrl
                .PostJsonAsync(listing)
                .ReceiveJson<int>()
                .Result;

            Assert.AreNotEqual(0, id);

            var response = _serviceUrl
                .AppendPathSegments(id)
                .DeleteAsync()
                .Result;

            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
