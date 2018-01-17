using AutoMapper.QueryableExtensions;
using BookTravel.Services.Admin;
using BookTravel.Web.Areas.Admin.Models;
using BookTravel.Web.Areas.Admin.Models.TransferTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BookTravel.Data.DataConstants;

namespace BookTravel.Web.Areas.Admin.Controllers
{
    public class TransferTypesController : BaseController
    {
        private readonly IAdminTransferTypeService types;
        public TransferTypesController(IAdminTransferTypeService types)
        {
            this.types = types;
        }

        public IActionResult AllTransferTypes()
        {
            var transferTypes = this.types.GetAllTransferTypes().AsQueryable();

            return View(transferTypes
                .ProjectTo<TransferTypeViewModel>()
                .ToList());
        }

        public IActionResult NewTransferType()
        {
            return View(new AddTransferTypeViewModel());
        }

        [HttpPost]
        public IActionResult NewTransferType(AddTransferTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool result = this.types.AddTransferType(
                model.Title,
                model.Price,
                model.MaxPeopleCount,
                model.IsOneWay
                );

            if (result)
            {
                TempData[SuccessMessageKey] = $"{model.Title} successfuly created";
            }
            else
            {
                TempData[ErrorMessageKey] = $"{model.Title} not created";
            }

            return RedirectToAction(nameof(AllTransferTypes));
        }

        public IActionResult EditTransferType(int typeId)
        {
            var transferType = this.types.GetTransferTypeById(typeId)
                .ProjectTo<TransferTypeViewModel>()
                .FirstOrDefault();

            if (transferType == null)
            {
                TempData[ErrorMessageKey] = "Transfer Type not found";
            }

            return View(transferType);
        }

        [HttpPost]
        public IActionResult EditTransferType(TransferTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData[ErrorMessageKey] = "Invalid data";
                return View(model);
            }

            bool result = this.types.EditTransferType(
                model.Id,
                model.Title,
                model.Price,
                model.MaxPeopleCount,
                model.IsOneWay
                );

            if (result)
            {
                TempData[SuccessMessageKey] = "Type successfuly updated";
                return RedirectToAction(nameof(AllTransferTypes));
            }
            else
            {
                TempData[ErrorMessageKey] = "Type not updated";
                return View(model);
            }
        }

        public IActionResult DeleteTransferType(int typeId, string typeTitle)
        {
            if (typeTitle == null)
            {
                TempData[ErrorMessageKey] = "Invalid Type";
                return RedirectToAction(nameof(AllTransferTypes));
            }
            return View(new DeleteTransferTypeViewModel
            {
                TypeId = typeId,
                TypeTitle = typeTitle
            });
        }

        public IActionResult DestroyType(int typeId)
        {

            bool result = this.types.DeleteTransferType(typeId);

            if(result)
            {
                TempData[SuccessMessageKey] = "Transfer type successfuly deleted";
            }
            else
            {
                TempData[ErrorMessageKey] = "Invalid Type";
            }
            return RedirectToAction(nameof(AllTransferTypes));
        }
    }
}
