using BookTravel.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookTravel.Services
{
    public interface IUserTransferService
    {
        IEnumerable<string> AirlinesNames();

        IEnumerable<string> AirportsNames();

        IEnumerable<string> DestinationsNames();

        int GetTransferTypeIdByName(string name);

        TransferTypeServiceModel GetTransferType(int id);

        IEnumerable<string> TransferTypeNames();

        Task<bool> AddTransfer(string subject, string klientName, string email, string phone, string destination, int arrivalPassengers, int babyPassengers, int holdBags, int skiBags, int snowboardBags, string pickupLocation, DateTime arrivalDate, DateTime arrivalTime, string arrivalFlightNumber, string arrivalAirline, string arrivalAirport, string additionalInformation, int transferTypeId);

        Task<bool> AddTransfer(string subject, string klientName, string email, string phone, string destination, int arrivalPassengers, int babyPassengers, int holdBags, int skiBags, int snowboardBags, string pickupLocation, DateTime arrivalDate, DateTime arrivalTime, string arrivalFlightNumber, string arrivalAirline, string arrivalAirport, string additionalInformation, int transferTypeId, int returnPassengers, DateTime departureDate, DateTime departureTime, string departureFlightNumber, string departureAirline, string departureAirport);
    }
}
