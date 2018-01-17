using AutoMapper.QueryableExtensions;
using BookTravel.Services.Admin;
using BookTravel.Web.Areas.Admin.Models.Airports;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using static BookTravel.Data.DataConstants;

namespace BookTravel.Web.Areas.Admin.Controllers
{
    public class AirportsController : BaseController
    {
        private readonly IAirportService airports;
        public AirportsController(IAirportService airports)
        {
            this.airports = airports;
        }

        public IActionResult AllAirports()
        {
            var result = this.airports.GetAirports();
            return View(result);
        }

        public IActionResult NewAirport()
        {
            return View(new NewAirportViewModel());
        }

        [HttpPost]
        public IActionResult NewAirport(NewAirportViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = this.airports.AddAirport(model.Name);

            if (result)
            {
                TempData[SuccessMessageKey] = "Airport added successfuly";
                return RedirectToAction(nameof(AllAirports));
            }
            else
            {
                TempData[ErrorMessageKey] = "Invalid data";
                return View(model);
            }
        }

        public IActionResult EditAirport(int airportId)
        {
            var airport = this.airports.GetAirportById(airportId)
                .ProjectTo<AirportViewModel>()
                .FirstOrDefault();

            if (airport == null)
            {
                TempData[ErrorMessageKey] = "No such airport!";
                return RedirectToAction(nameof(AllAirports));
            }

            return View(airport);
        }

        [HttpPost]
        public IActionResult EditAirport(AirportViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool result = this.airports.EditAirport(model.Id, model.Name);

            if (!result)
            {
                TempData[ErrorMessageKey] = "No such airport!";
            }
            else
            {
                TempData[SuccessMessageKey] = $"Airport {model.Name} successfuly edited";
            }

            return RedirectToAction(nameof(AllAirports));
        }

        public IActionResult DeleteAirport(int airportId)
        {
            var airport = this.airports.GetAirportById(airportId)
                .ProjectTo<AirportViewModel>()
                .FirstOrDefault();

            if (airport == null)
            {
                TempData[ErrorMessageKey] = "No such airport!";
                return RedirectToAction(nameof(AllAirports));
            }

            return View(airport);
        }

        [HttpPost]
        public IActionResult DestroyAirport(int airportId)
        {
            var result = this.airports.DeleteAirport(airportId);

            if (result)
            {
                TempData[SuccessMessageKey] = "Airport deleted successfuly";
            }
            else
            {
                TempData[ErrorMessageKey] = "Invalid data";
            }

            return RedirectToAction(nameof(AllAirports));
        }

    }
}
