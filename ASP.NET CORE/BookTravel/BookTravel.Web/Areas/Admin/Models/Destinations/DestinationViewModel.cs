using BookTravel.Common.Mapping;
using BookTravel.Services.Admin.Models;

namespace BookTravel.Web.Areas.Admin.Models.Destinations
{
    public class DestinationViewModel : NewDestinationViewModel, IMapFrom<DestinationServiceModel>
    {
        public int Id { get; set; }
    }
}
