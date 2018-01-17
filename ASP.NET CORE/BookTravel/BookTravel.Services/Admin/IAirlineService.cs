using BookTravel.Services.Admin.Models;
using System.Collections.Generic;
using System.Linq;

namespace BookTravel.Services.Admin
{
    public interface IAirlineService
    {
        IEnumerable<AirlineServiceModel> GetAirlines();

        bool AddAirline(string name);

        IQueryable<AirlineServiceModel> GetAirlineById(int airlineId);

        bool EditAirlines(int id, string name);

        bool DeleteAirlines(int id);

    }
}
