using AutoMapper.QueryableExtensions;
using BookTravel.Services.Admin;
using BookTravel.Web.Areas.Admin.Models.Destinations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BookTravel.Data.DataConstants;

namespace BookTravel.Web.Areas.Admin.Controllers
{
    public class DestinationsController : BaseController
    {
        private readonly IAdminDestinationService destinations;
        public DestinationsController(IAdminDestinationService destinations)
        {
            this.destinations = destinations;
        }

        public IActionResult AllDestinations()
        {
            var result = this.destinations.GetDestinations();
            return View(result);
        }

        public IActionResult NewDestination()
        {
            return View(new NewDestinationViewModel());
        }

        [HttpPost]
        public IActionResult NewDestination(NewDestinationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = this.destinations.AddDestination(model.Name);

            if (result)
            {
                TempData[SuccessMessageKey] = "Destination added successfuly";
                return RedirectToAction(nameof(AllDestinations));
            }
            else
            {
                TempData[ErrorMessageKey] = "Invalid data";
                return View(model);
            }
        }

        public IActionResult EditDestination(int destinationId)
        {
            var destination = this.destinations.GetDestinationById(destinationId)
                .ProjectTo<DestinationViewModel>()
                .FirstOrDefault();

            if (destination == null)
            {
                TempData[ErrorMessageKey] = "No such destination!";
                return RedirectToAction(nameof(AllDestinations));
            }

            return View(destination);
        }

        [HttpPost]
        public IActionResult EditDestination(DestinationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool result = this.destinations.EditDestination(model.Id, model.Name);

            if (!result)
            {
                TempData[ErrorMessageKey] = "No such destination!";
            }
            else
            {
                TempData[SuccessMessageKey] = $"Destination {model.Name} successfuly edited";
            }

            return RedirectToAction(nameof(AllDestinations));
        }

        public IActionResult DeleteDestination(int destinationId)
        {
            var destination = this.destinations.GetDestinationById(destinationId)
                .ProjectTo<DestinationViewModel>()
                .FirstOrDefault();

            if (destination == null)
            {
                TempData[ErrorMessageKey] = "No such destination!";
                return RedirectToAction(nameof(AllDestinations));
            }

            return View(destination);
        }

        [HttpPost]
        public IActionResult DestroyDestination(int destinationId)
        {
            var result = this.destinations.DeleteDestination(destinationId);

            if (result)
            {
                TempData[SuccessMessageKey] = "Destination deleted successfuly";
            }
            else
            {
                TempData[ErrorMessageKey] = "Invalid data";
            }

            return RedirectToAction(nameof(AllDestinations));
        }
    }
}
