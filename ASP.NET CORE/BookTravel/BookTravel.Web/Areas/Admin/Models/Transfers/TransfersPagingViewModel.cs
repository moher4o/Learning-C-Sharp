using BookTravel.Services.Admin.Models;
using System.Collections.Generic;

namespace BookTravel.Web.Areas.Admin.Models.Transfers
{
    public class TransfersPagingViewModel
    {
        public IEnumerable<AdminTransferListingServiceModel> Transfers { get; set; }

        public int CurrentPage { get; set; }

        public int PreviousPage => this.CurrentPage == 1 ? this.CurrentPage : this.CurrentPage - 1;

        public int NextPage => this.CurrentPage == this.TotalPages ? this.CurrentPage : this.CurrentPage + 1;

        public int TotalPages { get; set; }
    }
}
