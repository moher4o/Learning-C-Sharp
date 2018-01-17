using BookTravel.Services.Admin.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookTravel.Services.Admin
{
    public interface IAdminTransferService
    {
        Task<IEnumerable<AdminTransferListingServiceModel>> AllTransfers(int page = 1);

        int TotalPagesWithTransfers();

        AdminTransferListingServiceModel GetTransferById(int transferId);

        Task<byte[]> GetPdfTransfer(int transferId);
    }
}
