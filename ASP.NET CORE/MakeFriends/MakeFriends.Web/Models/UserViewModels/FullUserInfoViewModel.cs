using MakeFriends.Common.Mapping;
using MakeFriends.Data.Models.Enums;
using MakeFriends.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakeFriends.Web.Models.UserViewModels
{
    public class FullUserInfoViewModel : IMapFrom<FullUserInfoServiceModel>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public string Description { get; set; }

        public string ShortView { get; set; }

        public EyesColor? EyesColor { get; set; }

        public HairColor? HairColor { get; set; }

        public bool? IsSmoking { get; set; }

        public bool? IsDrinking { get; set; }

        public Sexuality? Sexuality { get; set; }

        public Relations? Relations { get; set; }

        public IEnumerable<string> Photos { get; set; }
    }
}
