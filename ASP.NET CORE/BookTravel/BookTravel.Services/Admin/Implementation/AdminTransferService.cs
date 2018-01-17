using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using BookTravel.Data;
using BookTravel.Data.Models;
using BookTravel.Services.Admin.Models;
using Microsoft.EntityFrameworkCore;
using static BookTravel.Data.DataConstants;

namespace BookTravel.Services.Admin.Implementation
{
    public class AdminTransferService : IAdminTransferService
    {
        private readonly TravelDbContext db;
        private readonly IPdfGenerator pdfGenerator;

        public AdminTransferService(TravelDbContext db, IPdfGenerator pdfGenerator)
        {
            this.db = db;
            this.pdfGenerator = pdfGenerator;
        }

        public async Task<IEnumerable<AdminTransferListingServiceModel>> AllTransfers(int page = 1)
        {
            //Task.Run(async () =>
            //{
            //}).Wait();

            var selectedTransfers = this.db.Transfers
                .OrderByDescending(u => u.ArrivalDate)
                .Skip((page - 1) * TransfersPerPageCount)
                .Take(TransfersPerPageCount)
                .ProjectTo<AdminTransferListingServiceModel>()
                .ToList();

            return selectedTransfers;
        }

        public AdminTransferListingServiceModel GetTransferById(int transferId)
        {
            var transfer = this.db.Transfers
                .Where(t => t.Id == transferId)
                .ProjectTo<AdminTransferListingServiceModel>()
                .FirstOrDefault();

            if (transfer == null)
            {
                return null;
            }

            return transfer;
        }

        public int TotalPagesWithTransfers()
        {
            return (int)Math.Ceiling(this.db.Transfers.Count() / (decimal)TransfersPerPageCount);
        }

        public async Task<byte[]> GetPdfTransfer(int transferId)
        {
            var transfer = this.db.FindAsync<Transfer>(transferId);

            if (transfer == null)
            {
                return null;
            }

            var transferInfo = await this.db
                .Transfers
                .Where(t => t.Id == transferId)
                .Select(t => new
                {
                    t.Id,
                     t.KlientName,
                     t.Subject,
                     t.Email,
                     t.Phone,
                    TransferTypeName = t.TransferType.Title,    
                    t.ArrivalPassengers,
                    t.ReturnPassengers,
                    t.BabyPassengers,
                    DestinationName = t.Destination.Name,
                    t.HoldBags,
                    t.SkiBags,
                    t.SnowboardBags,
                    t.PickupLocation,
                    ArrivalAirport = t.ArrivalAirport.Name,
                    ArrivalAirline = t.ArrivalAirline.Name,
                    t.ArrivalFlightNumber,
                    t.ArrivalDate,
                    DepartureAirport = t.DepartureAirport.Name,
                    DepartureAirline = t.DepartureAirline.Name,
                    t.DepartureFlightNumber,
                    t.DepartureDate,
                    t.AdditionalInformation,
                    t.RegistrationDate
                }).FirstOrDefaultAsync();

            return this.pdfGenerator.GeneratePdfFromHtml(string.Format(
                PdfCertificateFormat,
                transferInfo.Id,
                transferInfo.KlientName,
                transferInfo.TransferTypeName,
                transferInfo.Subject,
                transferInfo.Email,
                transferInfo.Phone,
                transferInfo.ArrivalPassengers,
                transferInfo.ReturnPassengers,
                transferInfo.BabyPassengers,
                transferInfo.DestinationName,
                transferInfo.HoldBags,
                transferInfo.SkiBags,
                transferInfo.SnowboardBags,
                transferInfo.ArrivalDate.ToString(),
                transferInfo.ArrivalAirline,
                transferInfo.ArrivalFlightNumber,
                transferInfo.ArrivalAirport,
                transferInfo.DepartureDate.ToString(),
                transferInfo.DepartureAirline,
                transferInfo.DepartureFlightNumber,
                transferInfo.DepartureAirport,
                transferInfo.AdditionalInformation,
                transferInfo.RegistrationDate.ToString(),
                transferInfo.PickupLocation
                ));
        }

    }
}
