using BookTravel.Common.Mapping;
using BookTravel.Services.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookTravel.Web.Areas.Admin.Models.TransferTypes
{
    public class TransferTypeViewModel : IMapFrom<TransferTypeServiceModel>
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Range(0, 100000)]
        public double Price { get; set; }

        [Range(0, 1000)]
        [Display(Name = "Max People Count")]
        public int MaxPeopleCount { get; set; }

        [Display(Name = "Is it one way")]
        public bool IsOneWay { get; set; }
    }
}
