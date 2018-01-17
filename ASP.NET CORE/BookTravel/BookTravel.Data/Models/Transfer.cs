using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookTravel.Data.Models
{
    public class Transfer
    {
        public int Id { get; set; }

        [MaxLength(200)]
        public string Subject { get; set; }

        [Required]
        [MaxLength(100)]
        public string KlientName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(200)]
        public string Email { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        public DateTime RegistrationDate { get; set; }

        public int? DestinationId { get; set; }

        public Destination Destination { get; set; }

        public int ArrivalPassengers { get; set; }

        public int ReturnPassengers { get; set; }

        public int BabyPassengers { get; set; }

        public int HoldBags { get; set; }

        public int SkiBags { get; set; }

        public int SnowboardBags { get; set; }

        [MaxLength(200)]
        public string PickupLocation { get; set; }

        public DateTime? ArrivalDate { get; set; }

        [MaxLength(50)]
        public string ArrivalFlightNumber { get; set; }

        public int? ArrivalAirlineId { get; set; }

        public Airline ArrivalAirline { get; set; }

        public int? ArrivalAirportId { get; set; }

        public Airport ArrivalAirport { get; set; }

        public DateTime? DepartureDate { get; set; }

        [MaxLength(50)]
        public string DepartureFlightNumber { get; set; }

        public int? DepartureAirlineId { get; set; }

        public Airline DepartureAirline { get; set; }

        public int? DepartureAirportId { get; set; }

        public Airport DepartureAirport { get; set; }

        [MaxLength(300)]
        public string AdditionalInformation { get; set; }

        public int TransferTypeId { get; set; }

        public TransferType TransferType { get; set; }
    }
}
