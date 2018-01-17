using BookTravel.Services;
using BookTravel.Services.Models;
using BookTravel.Web.Models.Transfers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BookTravel.Data.DataConstants;

namespace BookTravel.Web.Controllers
{
    public class TransfersController : Controller
    {
        private readonly IUserTransferService transfers;

        public TransfersController(IUserTransferService transfers)
        {
            this.transfers = transfers;
        }

        public IActionResult SelectTransferType()
        {
            var transferTypesNames = new SelectTransferViewModel();

            transferTypesNames.TransferNames = this.transfers.TransferTypeNames()
                   .Select(a => new SelectListItem
                   {
                       Text = a,
                       Value = a
                   })
                   .ToList();

            return View(transferTypesNames);
        }

        [HttpPost]
        public IActionResult SelectTransferType(string transferName)
        {
            return RedirectToAction(nameof(AddTransfer), new { transferTypeId = this.transfers.GetTransferTypeIdByName(transferName) });
        }


        public IActionResult AddTransfer(int transferTypeId)
        {
            var transferType = this.transfers.GetTransferType(transferTypeId);

            if (transferType == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var transfer = new AddTransferViewModel();
            return View(this.ModelPrepareForView(transfer, transferType));
        }

        [HttpPost]
        public async Task<IActionResult> AddTransfer(AddTransferViewModel model, int transferTypeId, string destination, int arrivalPassengers, int babyPassengers, string arrivalAirport, string arrivalAirline, int holdBags, int skiBags, int snowboardBags, int returnPassengers, string departureAirline, string departureAirport)
        {
            var transferType = this.transfers.GetTransferType(transferTypeId);
            if (transferType == null)
            {
                TempData[ErrorMessageKey] = "Invalid Transfer type!";
                return RedirectToAction(nameof(SelectTransferType));
            }

            if (!ModelState.IsValid)
            {
                return View(this.ModelPrepareForView(model, transferType));
            }

            if (model.ArrivalDate < DateTime.UtcNow.Date)
            {
                TempData[ErrorMessageKey] = "Invalid Arrival Date!";
                return View(this.ModelPrepareForView(model, transferType));
            }

            var result = true;

            if (transferType.IsOneWay)
            {
                result = await this.transfers.AddTransfer(
                    model.Subject, model.KlientName, model.Email, model.Phone, destination, arrivalPassengers, babyPassengers, holdBags, skiBags, snowboardBags, model.PickupLocation, model.ArrivalDate, model.ArrivalTime, model.ArrivalFlightNumber, arrivalAirline, arrivalAirport, model.AdditionalInformation, transferTypeId);
            }
            else
            {
                if (model.ArrivalDate > model.DepartureDate)
                {
                    TempData[ErrorMessageKey] = "Invalid Arrival or Departure Date!";
                    return View(this.ModelPrepareForView(model, transferType));
                }
                result = await this.transfers.AddTransfer(
                    model.Subject, model.KlientName, model.Email, model.Phone, destination, arrivalPassengers, babyPassengers, holdBags, skiBags, snowboardBags, model.PickupLocation, model.ArrivalDate, model.ArrivalTime, model.ArrivalFlightNumber, arrivalAirline, arrivalAirport, model.AdditionalInformation, transferTypeId, returnPassengers, model.DepartureDate, model.DepartureTime, model.DepartureFlightNumber, departureAirline, departureAirport);
            }


            if (result)
            {
                TempData[SuccessMessageKey] = $"Transfer type - {transferType.Title} - successfuly send";
            }
            else
            {
                TempData[ErrorMessageKey] = "Transfer not send! Check you data!";
                return View(this.ModelPrepareForView(model, transferType));
            }

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        private AddTransferViewModel ModelPrepareForView(AddTransferViewModel model, TransferTypeServiceModel transferType)
        {
            var dropdownCount = new List<SelectListItem>();
            for (int i = 0; i <= transferType.MaxPeopleCount; i++)
            {
                dropdownCount.Add(new SelectListItem
                {
                    Text = i.ToString(),
                    Value = i.ToString()
                });
            }

            model.ArrivalDate = DateTime.UtcNow.Date;
            model.ArrivalTime = DateTime.UtcNow.Date;
            model.DepartureDate = DateTime.UtcNow.Date.AddDays(1);
            model.DepartureTime = DateTime.UtcNow.Date.AddDays(1);
            model.ArrivalPassengers = dropdownCount.Where(d => d.Value != "0");
            model.BabyPassengers = dropdownCount;
            model.HoldBags = dropdownCount;
            model.SkiBags = dropdownCount;
            model.SnowboardBags = dropdownCount;
            model.ReturnPassengers = dropdownCount.Where(d => d.Value != "0");
            model.ArrivalAirline = this.transfers.AirlinesNames()
               .Select(a => new SelectListItem
               {
                   Text = a,
                   Value = a
               })
               .ToList();
            model.DepartureAirline = this.transfers.AirlinesNames()
               .Select(a => new SelectListItem
               {
                   Text = a,
                   Value = a
               })
               .ToList();
            model.ArrivalAirport = this.transfers.AirportsNames()
               .Select(a => new SelectListItem
               {
                   Text = a,
                   Value = a
               })
               .ToList();
            model.DepartureAirport = this.transfers.AirportsNames()
               .Select(a => new SelectListItem
               {
                   Text = a,
                   Value = a
               })
               .ToList();
            model.Destination = this.transfers.DestinationsNames()
               .Select(a => new SelectListItem
               {
                   Text = a,
                   Value = a
               })
               .ToList();
            model.TransferType = transferType;

            return model;
        }

    }
}
