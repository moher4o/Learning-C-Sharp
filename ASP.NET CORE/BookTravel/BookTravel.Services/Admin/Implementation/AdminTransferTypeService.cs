using AutoMapper.QueryableExtensions;
using BookTravel.Data;
using BookTravel.Data.Models;
using BookTravel.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookTravel.Services.Admin.Implementation
{
    public class AdminTransferTypeService : IAdminTransferTypeService
    {
        private readonly TravelDbContext db;

        public AdminTransferTypeService(TravelDbContext db)
        {
            this.db = db;
        }

        public bool AddTransferType(string title, double price, int maxPeopleCount, bool isOneWay)
        {
            if (title == null)
            {
                return false;
            }
            var newTransferType = new TransferType()
            {
                Title = title,
                Price = price,
                MaxPeopleCount = maxPeopleCount,
                IsOneWay = isOneWay
            };
            this.db.TransferTypes.Add(newTransferType);
            this.db.SaveChanges();

            return true;
        }

        public bool DeleteTransferType(int typeId)
        {
            var currentType = this.db.TransferTypes
                 .FirstOrDefault(t => t.Id == typeId);

            if (currentType == null)
            {
                return false;
            }

            this.db.TransferTypes.Remove(currentType);
            this.db.SaveChanges();
            return true;
        }

        public bool EditTransferType(int typeId, string title, double price, int maxPeopleCount, bool isOneWay)
        {
            var currentType = this.db.TransferTypes
                .FirstOrDefault(t => t.Id == typeId);

            if (currentType == null || title == null)
            {
                return false;
            }

            currentType.Title = title;
            currentType.Price = price;
            currentType.MaxPeopleCount = maxPeopleCount;
            currentType.IsOneWay = isOneWay;

            this.db.SaveChanges();

            return true;
        }

        public IEnumerable<TransferTypeServiceModel> GetAllTransferTypes()
        {
           return this.db.TransferTypes
                    .ProjectTo<TransferTypeServiceModel>()
                    .ToList();
        }

        public IQueryable<TransferTypeServiceModel> GetTransferTypeById(int id)
        {
            var transferType = this.db.TransferTypes
                .Where(t => t.Id == id)
                .ProjectTo<TransferTypeServiceModel>();

            if (transferType == null)
            {
                return null;
            }

            return transferType;
        }
    }
}
