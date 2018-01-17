using BookTravel.Common.Mapping;
using BookTravel.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookTravel.Services.Models
{
   public class TransferTypeServiceModel : IMapFrom<TransferType>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public double Price { get; set; }

        public int MaxPeopleCount { get; set; }

        public bool IsOneWay { get; set; }
    }
}
