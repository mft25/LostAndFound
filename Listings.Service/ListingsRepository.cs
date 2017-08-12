using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace Listings.Service
{
    public class ListingsRepository : IListingsRepository
    {
        private readonly string _connectionString;

        public ListingsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public PublicListing Get(int id)
        {
            const string sql = @"
SELECT
    Id,
    Text
FROM
    Listings
WHERE
    Disabled = 0
    AND Id = @id
";

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection
                    .Query<PublicListing>(sql, new { id })
                    .SingleOrDefault();
            }
        }

        public IList<PublicListing> GetAll()
        {
            const string sql = @"
SELECT
    Id,
    Text
FROM
    Listings
WHERE
    Disabled = 0
";

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection
                    .Query<PublicListing>(sql)
                    .ToList();
            }
        }

        public int Add(Listing listing)
        {
            const string sql = @"
INSERT INTO Listings (
    Text
)
VALUES (
    @text
)

SELECT
    SCOPE_IDENTITY()
";

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection
                    .Query<int>(sql, listing)
                    .Single();
            }
        }

        public void Update(int id, Listing listing)
        {
            var input = new DynamicParameters(listing);
            input.Add("id", id);

            const string sql = @"
UPDATE
    Listings
SET
    Text = @text
WHERE
    Id = @id
";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(sql, input);
            }
        }

        public void Delete(int id)
        {
            const string sql = @"
UPDATE
    Listings
SET
    Disabled = 1
WHERE
    Id = @id
";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(sql, new { id });
            }
        }
    }
}
