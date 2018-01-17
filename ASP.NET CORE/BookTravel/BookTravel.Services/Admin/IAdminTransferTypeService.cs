using BookTravel.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookTravel.Services.Admin
{
   public interface IAdminTransferTypeService
    {
        IEnumerable<TransferTypeServiceModel> GetAllTransferTypes();

        bool AddTransferType(string title, double price, int maxPeopleCount, bool isOneWay);

        IQueryable<TransferTypeServiceModel> GetTransferTypeById(int id);

        bool EditTransferType(int typeId, string title, double price, int maxPeopleCount, bool isOneWay);

        bool DeleteTransferType(int typeId);
    }
}
