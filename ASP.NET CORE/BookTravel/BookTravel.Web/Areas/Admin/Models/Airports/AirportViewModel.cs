using BookTravel.Common.Mapping;
using BookTravel.Services.Admin.Models;

namespace BookTravel.Web.Areas.Admin.Models.Airports
{
    public class AirportViewModel : AdminViewModel, IMapFrom<AirportServiceModel>
    {
    }
}
