using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BookTravel.Web.Models.Transfers
{
    public class SelectTransferViewModel
    {
        public IEnumerable<SelectListItem> TransferNames { get; set; }
    }
}
