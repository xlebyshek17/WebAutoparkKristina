using AutoMapper;
using AutoparkWebEF.BLL.DTO;
using AutoparkWebEF.BLL.Interfaces;
using AutoparkWebEF.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoparkWebEF.Controllers
{
    public class VehicleTypeViewModelController : Controller
    {
        IService<VehicleTypeDto> db;
        private readonly IMapper _mapper;

        public VehicleTypeViewModelController(IService<VehicleTypeDto> service, IMapper mapper)
        {
            db = service;
            _mapper = mapper;
        }

        public IActionResult ViewVehicleTypes()
        {
            var vehicleTypeDtos = db.GetAll();
            var vehicles = _mapper.Map<IEnumerable<VehicleTypeDto>, List<VehicleTypeViewModel>>(vehicleTypeDtos);
            return View(vehicles);
        }

        public IActionResult CreateType()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateType(VehicleTypeViewModel type)
        {
            var typeDto = _mapper.Map<VehicleTypeViewModel, VehicleTypeDto>(type);

            db.Create(typeDto);

            return RedirectToAction("ViewVehicleTypes");
        }

        [HttpGet]
        [ActionName("DeleteType")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                var typeDto = await db.Get(id);
                var type = _mapper.Map<VehicleTypeDto, VehicleTypeViewModel>(typeDto);
                if (type != null)
                    return View(type);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteType(int? id)
        {
            if (id != null)
            {
                var typeDto = await db.Get(id);
                if (typeDto != null)
                {
                    db.Delete(typeDto);
                    return RedirectToAction("ViewVehicleTypes");
                }
            }
            return NotFound();
        }

        public async Task<IActionResult> EditType(int? id)
        {
            if (id != null)
            {
                var typeDto = await db.Get(id);
                var type = _mapper.Map<VehicleTypeDto, VehicleTypeViewModel>(typeDto);
                if (type != null)
                    return View(type);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult EditType(VehicleTypeViewModel type)
        {
            if (ModelState.IsValid)
            {
                var typeDto = _mapper.Map<VehicleTypeViewModel, VehicleTypeDto>(type);
                db.Update(typeDto);
                return RedirectToAction("ViewVehicleTypes");
            }
            else
                return RedirectToAction("EditType");
        }
    }
}
