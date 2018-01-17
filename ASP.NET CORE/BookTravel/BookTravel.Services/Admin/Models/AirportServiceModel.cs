using BookTravel.Common.Mapping;
using BookTravel.Data.Models;

namespace BookTravel.Services.Admin.Models
{
    public class AirportServiceModel : IMapFrom<Airport>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
