using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Posts.Service
{
    [RoutePrefix("api/listings")]
    public class ListingsController : ApiController
    {
        private readonly List<Listing> _listings;

        private ListingsController()
        {
            _listings = new List<Listing>
            {
                new Listing { Id = 1, Text = "aardvark apple anus" },
                new Listing { Id = 2, Text = "beetle boulanger bastard" },
                new Listing { Id = 3, Text = "catnip cod cunt", Disabled = true }
            };
        }

        [HttpGet]
        [Route("")]
        public IList<Listing> GetAll()
        {
            return _listings
                .Where(l => !l.Disabled)
                .ToList();
        }

        [HttpGet]
        [Route("{id:int}")]
        public Listing Get(int id)
        {
            var listing = _listings.FirstOrDefault(l => l.Id == id && !l.Disabled);

            if (listing == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return listing;
        }

        [HttpPost]
        [Route("")]
        public int Post([FromBody]NewListing listing)
        {
            var id = _listings.Max(l => l.Id) + 1;

            _listings.Add(new Listing
            {
                Id = id,
                Text = listing.Text,
                Disabled = false
            });

            return id;
        }

        [HttpPut]
        [Route("{id:int}")]
        public void Put(int id, [FromBody]NewListing newListing)
        {
            var listing = _listings.FirstOrDefault(l => l.Id == id && !l.Disabled);

            if (listing == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            listing.Text = newListing.Text;
        }

        [HttpDelete]
        [Route("{id:int}")]
        public void Delete(int id)
        {
            var listing = _listings.FirstOrDefault(l => l.Id == id && !l.Disabled);

            if (listing == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            listing.Disabled = true;
        }
    }

    public class Listing : NewListing
    {
        public int Id { get; set; }
        public bool Disabled { get; set; }
    }

    public class NewListing
    {
        public string Text { get; set; }
    }
}
