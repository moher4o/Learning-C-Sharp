using BookTravel.Data.Models;
using BookTravel.Services.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookTravel.Web.Models.Transfers
{
    public class AddTransferViewModel
    {
        [MaxLength(200)]
        public string Subject { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Your Name*")]
        public string KlientName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(200)]
        [Display(Name = "Your Email*")]
        public string Email { get; set; }

        [MaxLength(20)]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        public IEnumerable<SelectListItem> Destination { get; set; }

        [Display(Name = "Number of passengers")]
        public IEnumerable<SelectListItem> ArrivalPassengers { get; set; }


        [Display(Name = "Number of return passengers")]
        public IEnumerable<SelectListItem> ReturnPassengers { get; set; }


        [Display(Name = "Baby in car seat")]
        public IEnumerable<SelectListItem> BabyPassengers { get; set; }

        [Display(Name = "Number of Hold Bags")]
        //[Range(0, 40)]
        public IEnumerable<SelectListItem> HoldBags { get; set; }

        [Display(Name = "Number of Ski Bags")]
        //[Range(0, 20)]
        public IEnumerable<SelectListItem> SkiBags { get; set; }

        [Display(Name = "Number of Snowboard Bags")]
        //[Range(0, 20)]
        public IEnumerable<SelectListItem> SnowboardBags { get; set; }

        [MaxLength(200)]
        [Display(Name = "Pickup location(if not from Airport)")]
        public string PickupLocation { get; set; }

        [Display(Name = "Arrival Date")]
        [DataType(DataType.Date)]
        public DateTime ArrivalDate { get; set; }

        [Display(Name = "Arrival Time")]
        [DataType(DataType.Time)]
        public DateTime ArrivalTime { get; set; }

        [MaxLength(50)]
        [Display(Name = "Arrival Flight Number")]
        public string ArrivalFlightNumber { get; set; }

        [Display(Name = "Arrival Airline")]
        public IEnumerable<SelectListItem> ArrivalAirline { get; set; }

        [Display(Name = "Arrival Airport")]
        public IEnumerable<SelectListItem> ArrivalAirport { get; set; }

        [Display(Name = "Departure Date")]
        [DataType(DataType.Date)]
        public DateTime DepartureDate { get; set; }

        [Display(Name = "Departure Time")]
        [DataType(DataType.Time)]
        public DateTime DepartureTime { get; set; }

        [MaxLength(50)]
        [Display(Name = "Departure Flight Number")]
        public string DepartureFlightNumber { get; set; }

        [Display(Name = "Departure Airline")]
        public IEnumerable<SelectListItem> DepartureAirline { get; set; }

        [Display(Name = "Departure Airport")]
        public IEnumerable<SelectListItem> DepartureAirport { get; set; }

        [MaxLength(300)]
        [Display(Name = "Additional information")]
        public string AdditionalInformation { get; set; }

        public TransferTypeServiceModel TransferType { get; set; }

    }
}
