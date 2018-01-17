using BookTravel.Services.Admin.Models;
using System.Collections.Generic;
using System.Linq;

namespace BookTravel.Services.Admin
{
    public interface IAirportService
    {
        IEnumerable<AirportServiceModel> GetAirports();

        bool AddAirport(string name);

        IQueryable<AirportServiceModel> GetAirportById(int airportId);

        bool EditAirport(int id, string name);

        bool DeleteAirport(int id);
    }
}
