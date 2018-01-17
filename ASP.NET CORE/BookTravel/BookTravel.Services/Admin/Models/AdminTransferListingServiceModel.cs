using AutoMapper;
using BookTravel.Common.Mapping;
using BookTravel.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookTravel.Services.Admin.Models
{
    public class AdminTransferListingServiceModel : IMapFrom<Transfer>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Subject { get; set; }

        public string KlientName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string DestinationName { get; set; }

        public int ArrivalPassengers { get; set; }

        public int ReturnPassengers { get; set; }

        public int BabyPassengers { get; set; }

        public int HoldBags { get; set; }

        public int SkiBags { get; set; }

        public int SnowboardBags { get; set; }

        public string PickupLocation { get; set; }

        public DateTime ArrivalDate { get; set; }

        public string ArrivalFlightNumber { get; set; }

        public string ArrivalAirlineName { get; set; }

        public string ArrivalAirportName { get; set; }

        public DateTime DepartureDate { get; set; }

        public string DepartureFlightNumber { get; set; }

        public string DepartureAirlineName { get; set; }

        public string DepartureAirportName { get; set; }

        public string AdditionalInformation { get; set; }

        public string TransferTypeTitle { get; set; }

        public DateTime RegistrationDate { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<Transfer, AdminTransferListingServiceModel>()
                   .ForMember(u => u.DestinationName, cfg => cfg.MapFrom(t => t.Destination.Name))
                   .ForMember(u => u.ArrivalAirlineName, cfg => cfg.MapFrom(t => t.ArrivalAirline.Name))
                   .ForMember(u => u.ArrivalAirportName, cfg => cfg.MapFrom(t => t.ArrivalAirport.Name))
                   .ForMember(u => u.TransferTypeTitle, cfg => cfg.MapFrom(t => t.TransferType.Title))
                   .ForMember(u => u.DepartureAirlineName, cfg => cfg.MapFrom(t => t.DepartureAirline.Name))
                   .ForMember(u => u.DepartureAirportName, cfg => cfg.MapFrom(t => t.DepartureAirport.Name));
        }
    }
}
