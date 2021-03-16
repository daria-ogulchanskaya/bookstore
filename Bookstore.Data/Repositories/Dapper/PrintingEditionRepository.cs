using Bookstore.Data.Entities;
using Bookstore.Data.Repositories.Dapper.Interfaces;
using Bookstore.Shared.Models.Filter;
using Bookstore.Shared.Models.Page;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Bookstore.Data.Repositories.Dapper
{
    public class PrintingEditionRepository : Repository<PrintingEdition>
    {
        public PrintingEditionRepository(IConfiguration configuration)
            : base(configuration)
        { }

//        public async Task<IEnumerable<PrintingEdition>> GetPageAsync(PaginationRequest<PrintingEditionFilter> request)
//        {
//            IEnumerable<PrintingEdition> results;

//            using (var connection = new SqlConnection(_connectionString))
//            {
//                await connection.OpenAsync();
//                var query = @"
//SELECT f.*, sf.*
//FROM Flight f
//INNER JOIN ScheduledFlight sf ON f.ScheduledFlightId = sf.Id
//INNER JOIN Airport a ON sf.ArrivalAirportId = a.Id
//INNER JOIN Airport d ON sf.DepartureAirportId = d.Id
//WHERE a.Code = @AirportCode OR d.Code = @AirportCode
//ORDER BY f.Day, sf.FlightNumber
//OFFSET @Offset ROWS
//FETCH NEXT @PageSize ROWS ONLY;
//";
//            }

//            return results;
//        }

        public new async Task<PrintingEdition> AddAsync(PrintingEdition item)
        {
            throw new System.NotImplementedException();
        }
    }
}
