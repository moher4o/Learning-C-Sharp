using BookTravel.Services.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookTravel.Services.Admin
{
    public interface IAdminDestinationService
    {
        IEnumerable<DestinationServiceModel> GetDestinations();

        bool AddDestination(string name);

        IQueryable<DestinationServiceModel> GetDestinationById(int destinationId);

        bool EditDestination(int id, string name);

        bool DeleteDestination(int id);
    }
}
