using AutoMapper.QueryableExtensions;
using BookTravel.Services.Admin;
using BookTravel.Web.Areas.Admin.Models.Airlines;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using static BookTravel.Data.DataConstants;

namespace BookTravel.Web.Areas.Admin.Controllers
{
    public class AirlinesController : BaseController
    {
        private readonly IAirlineService airlines;
        public AirlinesController(IAirlineService airlines)
        {
            this.airlines = airlines;
        }

        public IActionResult AllAirlines()
        {
            var result = this.airlines.GetAirlines();
            return View(result);
        }

        public IActionResult NewAirline()
        {
            return View(new NewAirlineViewModel());
        }

        [HttpPost]
        public IActionResult NewAirline(NewAirlineViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = this.airlines.AddAirline(model.Name);

            if (result)
            {
                TempData[SuccessMessageKey] = "Airline added successfuly";
                return RedirectToAction(nameof(AllAirlines));
            }
            else
            {
                TempData[ErrorMessageKey] = "Invalid data";
                return View(model);
            }
        }

        public IActionResult EditAirline(int airlineId)
        {
            var airline = this.airlines.GetAirlineById(airlineId)
                .ProjectTo<AirlineViewModel>()
                .FirstOrDefault();

            if (airline == null)
            {
                TempData[ErrorMessageKey] = "No such airport!";
                return RedirectToAction(nameof(AllAirlines));
            }

            return View(airline);
        }

        [HttpPost]
        public IActionResult EditAirline(AirlineViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool result = this.airlines.EditAirlines(model.Id, model.Name);

            if (!result)
            {
                TempData[ErrorMessageKey] = "No such airline!";
            }
            else
            {
                TempData[SuccessMessageKey] = $"Airline {model.Name} successfuly edited";
            }

            return RedirectToAction(nameof(AllAirlines));
        }

        public IActionResult DeleteAirline(int airlineId)
        {
            var airline = this.airlines.GetAirlineById(airlineId)
                .ProjectTo<AirlineViewModel>()
                .FirstOrDefault();

            if (airline == null)
            {
                TempData[ErrorMessageKey] = "No such airline!";
                return RedirectToAction(nameof(AllAirlines));
            }

            return View(airline);
        }

        [HttpPost]
        public IActionResult DestroyAirline(int airlineId)
        {
            var result = this.airlines.DeleteAirlines(airlineId);

            if (result)
            {
                TempData[SuccessMessageKey] = "Airline deleted successfuly";
            }
            else
            {
                TempData[ErrorMessageKey] = "Invalid data";
            }

            return RedirectToAction(nameof(AllAirlines));
        }

    }
}
