using MakeFriends.Common.Mapping;
using MakeFriends.Services.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace MakeFriends.Web.Models.UserViewModels
{
    public class VisitorViewModel : UserListViewModel, IMapFrom<VisitorServiceModel>
    {
        [DataType(DataType.Date)]
        public DateTime VisitDate { get; set; }
    }
}
