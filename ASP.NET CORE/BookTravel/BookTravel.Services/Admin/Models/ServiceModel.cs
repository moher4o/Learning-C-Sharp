using System;
using System.Collections.Generic;
using System.Text;

namespace BookTravel.Services.Admin.Models
{
   public abstract class ServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
