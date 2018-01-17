using BookTravel.Services.Admin;
using BookTravel.Services.Admin.Models;
using BookTravel.Web.Areas.Admin.Models.Transfers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BookTravel.Data.DataConstants;

namespace BookTravel.Web.Areas.Admin.Controllers
{
    public class TransfersController : BaseController
    {
        private readonly IAdminTransferService transfers;
        public TransfersController(IAdminTransferService transfers)
        {
            this.transfers = transfers;
        }

        public async Task<IActionResult> AllTransfers(int page = 1)
        {
            var transfersPage = await this.transfers.AllTransfers(page);

            return View(new TransfersPagingViewModel
            {
                CurrentPage = page,
                Transfers = transfersPage,
                TotalPages = this.transfers.TotalPagesWithTransfers()
            });
        }

        public IActionResult TransferDetails(int transferId)
        {
            var transfer = this.transfers.GetTransferById(transferId);

            if (transfer == null)
            {
                TempData[ErrorMessageKey] = "No such Transfer";
                return RedirectToAction(nameof(AllTransfers), new { page = 1 });
            }

            return View(transfer);
        }

        public async Task<IActionResult> ExportTransferPdf(int transferId)
        {
            var transfer = this.transfers.GetTransferById(transferId);

            if (transfer == null)
            {
                TempData[ErrorMessageKey] = "No such Transfer";
                return RedirectToAction(nameof(AllTransfers), new { page = 1 });
            }

            var transferPdf = await this.transfers.GetPdfTransfer(transferId);
            if (transferPdf == null)
            {
                TempData[ErrorMessageKey] = "Pdf generation failed";
                return RedirectToAction(nameof(TransferDetails), new { transferId });
            }

            return File(transferPdf, "application/pdf");
        }
    }
}
