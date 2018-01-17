using AutoMapper.QueryableExtensions;
using BookTravel.Data;
using BookTravel.Data.Models;
using BookTravel.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTravel.Services.Implementation
{
    public class UserTransferService : IUserTransferService
    {
        private readonly TravelDbContext db;

        public UserTransferService(TravelDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> AddTransfer(string subject, string klientName, string email, string phone, string destination, int arrivalPassengers, int babyPassengers, int holdBags, int skiBags, int snowboardBags, string pickupLocation, DateTime arrivalDate, DateTime arrivalTime, string arrivalFlightNumber, string arrivalAirline, string arrivalAirport, string additionalInformation, int transferTypeId)
        {
            var transferType = this.db.TransferTypes.FirstOrDefault(t => t.Id == transferTypeId);
            var destinatinBorovec = this.db.Destinations.Where(d => d.Name == destination).FirstOrDefault();
            if (transferType == null || destinatinBorovec == null || (arrivalFlightNumber != null ? arrivalFlightNumber.Length > 50 : false) || (phone != null ? phone.Length > 20 : false) || email.Length > 200 || klientName.Length > 100 || (subject != null ? subject.Length > 200 : false) || (additionalInformation != null ? additionalInformation.Length > 300 : false))
            {
                return false;
            }

            if (pickupLocation == null)
            {
                var airport = this.db.Airports.FirstOrDefault(a => a.Name == arrivalAirport);
                var airline = this.db.Airlines.FirstOrDefault(a => a.Name == arrivalAirline);
                if (klientName == null || email == null || destination == null || airport == null || arrivalDate.Date < DateTime.UtcNow.Date)
                {
                    return false;
                }

                var newTransfer = new Transfer()
                {
                    KlientName = klientName,
                    Email = email,
                    Subject = subject,
                    Phone = phone,
                    DestinationId = destinatinBorovec.Id,
                    RegistrationDate = DateTime.UtcNow,
                    ArrivalPassengers = arrivalPassengers,
                    BabyPassengers = babyPassengers,
                    HoldBags = holdBags,
                    SkiBags = skiBags,
                    SnowboardBags = snowboardBags,
                    ArrivalDate = arrivalDate.Date + arrivalTime.TimeOfDay,
                    ArrivalFlightNumber = arrivalFlightNumber,
                    ArrivalAirportId = airport.Id,
                    ArrivalAirline = airline,
                    AdditionalInformation = additionalInformation,
                    TransferTypeId = transferType.Id
                };

                await this.db.Transfers.AddAsync(newTransfer);
                await this.db.SaveChangesAsync();
                return true;
            }
            else
            {
                if (klientName == null || email == null || destination == null || arrivalDate.Date < DateTime.UtcNow.Date || pickupLocation.Length > 200)
                {
                    return false;
                }

                var newTransfer = new Transfer()
                {
                    KlientName = klientName,
                    Email = email,
                    Subject = subject,
                    Phone = phone,
                    PickupLocation = pickupLocation,
                    DestinationId = destinatinBorovec.Id,
                    RegistrationDate = DateTime.UtcNow,
                    ArrivalPassengers = arrivalPassengers,
                    BabyPassengers = babyPassengers,
                    HoldBags = holdBags,
                    SkiBags = skiBags,
                    SnowboardBags = snowboardBags,
                    ArrivalDate = arrivalDate.Date + arrivalTime.TimeOfDay,
                    AdditionalInformation = additionalInformation,
                    TransferTypeId = transferType.Id
                };

                await this.db.Transfers.AddAsync(newTransfer);
                await this.db.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> AddTransfer(string subject, string klientName, string email, string phone, string destination, int arrivalPassengers, int babyPassengers, int holdBags, int skiBags, int snowboardBags, string pickupLocation, DateTime arrivalDate, DateTime arrivalTime, string arrivalFlightNumber, string arrivalAirline, string arrivalAirport, string additionalInformation, int transferTypeId, int returnPassengers, DateTime departureDate, DateTime departureTime, string departureFlightNumber, string departureAirline, string departureAirport)
        {
            var transferType = this.db.TransferTypes.FirstOrDefault(t => t.Id == transferTypeId);
            var destinatinBorovec = this.db.Destinations.Where(d => d.Name == destination).FirstOrDefault();
            var arrivalRealDate = arrivalDate.Date + arrivalTime.TimeOfDay;
            var departureRealDate = departureDate.Date + departureTime.TimeOfDay;
            if (transferType == null || destinatinBorovec == null || arrivalFlightNumber.Length > 50 || phone.Length > 20 || email.Length > 200 || klientName.Length > 100 || subject.Length > 200 || additionalInformation.Length > 300 || departureFlightNumber.Length > 50 || arrivalRealDate >= departureRealDate)
            {
                return false;
            }

            var departureRealAirport = this.db.Airports.FirstOrDefault(a => a.Name == departureAirport);
            var departureRealAirline = this.db.Airlines.FirstOrDefault(a => a.Name == departureAirline);

            if (pickupLocation == null)
            {
                var arrivalRealAirport = this.db.Airports.FirstOrDefault(a => a.Name == arrivalAirport);
                var arrivalRealAirline = this.db.Airlines.FirstOrDefault(a => a.Name == arrivalAirline);
                if (klientName == null || email == null || destination == null || arrivalRealAirport == null || arrivalDate < DateTime.UtcNow.Date)
                {
                    return false;
                }

                var newTransfer = new Transfer()
                {
                    KlientName = klientName,
                    Email = email,
                    Subject = subject,
                    Phone = phone,
                    DestinationId = destinatinBorovec.Id,
                    RegistrationDate = DateTime.UtcNow,
                    ArrivalPassengers = arrivalPassengers,
                    BabyPassengers = babyPassengers,
                    HoldBags = holdBags,
                    SkiBags = skiBags,
                    SnowboardBags = snowboardBags,
                    ArrivalDate = arrivalRealDate,
                    ArrivalFlightNumber = arrivalFlightNumber,
                    ArrivalAirportId = arrivalRealAirport.Id,
                    ArrivalAirline = arrivalRealAirline,
                    AdditionalInformation = additionalInformation,
                    TransferTypeId = transferType.Id,
                    DepartureDate = departureRealDate,
                    DepartureFlightNumber = departureFlightNumber,
                    DepartureAirport = departureRealAirport,
                    DepartureAirline = departureRealAirline,
                    ReturnPassengers = returnPassengers
                };

                await this.db.Transfers.AddAsync(newTransfer);
                await this.db.SaveChangesAsync();
                return true;
            }
            else
            {
                if (klientName == null || email == null || destination == null || arrivalDate < DateTime.UtcNow || pickupLocation.Length > 200)
                {
                    return false;
                }

                var newTransfer = new Transfer()
                {
                    KlientName = klientName,
                    Email = email,
                    Subject = subject,
                    Phone = phone,
                    PickupLocation = pickupLocation,
                    DestinationId = destinatinBorovec.Id,
                    RegistrationDate = DateTime.UtcNow,
                    ArrivalPassengers = arrivalPassengers,
                    BabyPassengers = babyPassengers,
                    HoldBags = holdBags,
                    SkiBags = skiBags,
                    SnowboardBags = snowboardBags,
                    ArrivalDate = arrivalDate.Date + arrivalTime.TimeOfDay,
                    AdditionalInformation = additionalInformation,
                    TransferTypeId = transferType.Id,
                    DepartureDate = departureDate.Date + departureTime.TimeOfDay,
                    DepartureFlightNumber = departureFlightNumber,
                    DepartureAirport = departureRealAirport,
                    DepartureAirline = departureRealAirline,
                    ReturnPassengers = returnPassengers
                };

                await this.db.Transfers.AddAsync(newTransfer);
                await this.db.SaveChangesAsync();
                return true;
            }
        }


        public IEnumerable<string> AirlinesNames()
        {
           return this.db.Airlines.OrderBy(a => a.Name).Select(a => a.Name).ToList();
        }

        public IEnumerable<string> AirportsNames()
        {
            return this.db.Airports.OrderBy(a => a.Name).Select(a => a.Name).ToList();
        }

        public IEnumerable<string> DestinationsNames()
        {
            return this.db.Destinations.OrderBy(a => a.Name).Select(a => a.Name).ToList();
        }

        public IEnumerable<string> TransferTypeNames()
        {
            return this.db.TransferTypes.OrderBy(a => a.Id).Select(a => a.Title).ToList();
        }

        public TransferTypeServiceModel GetTransferType(int id)
        {
            return this.db.TransferTypes
                .Where(t => t.Id == id)
                .ProjectTo<TransferTypeServiceModel>()
                .FirstOrDefault();
        }

        public int GetTransferTypeIdByName(string name)
        {
            return this.db.TransferTypes
                .Where(t => t.Title == name)
                .Select(t => t.Id)
                .FirstOrDefault();
        }
    }
}
