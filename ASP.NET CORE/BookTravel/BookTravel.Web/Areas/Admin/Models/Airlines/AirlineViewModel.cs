using BookTravel.Common.Mapping;
using BookTravel.Services.Admin.Models;

namespace BookTravel.Web.Areas.Admin.Models.Airlines
{
    public class AirlineViewModel : AdminViewModel, IMapFrom<AirlineServiceModel>
    {
    }
}
