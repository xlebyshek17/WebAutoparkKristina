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
    public class VehicleViewModelController : Controller
    {
        IService<VehicleDto> db;
        private readonly IMapper _mapper;

        public VehicleViewModelController(IService<VehicleDto> service, IMapper mapper)
        {
            db = service;
            _mapper = mapper;
        }

        public IActionResult ViewVehicles(SortState sortOrder = SortState.DefaultByID)
        {
            ViewData["ModelName"] = sortOrder == SortState.ModelNameAsc ? SortState.ModelNameDesc : SortState.ModelNameAsc;
            ViewData["Mileage"] = sortOrder == SortState.MileageAsc ? SortState.MileageDesc : SortState.MileageAsc;
            ViewData["TypeName"] = sortOrder == SortState.TypeNameAsc ? SortState.TypeNameDesc : SortState.TypeNameAsc;

            var vehicleDtos = db.GetAll();

            vehicleDtos = sortOrder switch
            {
                SortState.ModelNameAsc => vehicleDtos.OrderBy(v => v.ModelName),
                SortState.ModelNameDesc => vehicleDtos.OrderByDescending(v => v.ModelName),
                SortState.TypeNameAsc => vehicleDtos.OrderBy(v => v.Type.TypeName),
                SortState.TypeNameDesc => vehicleDtos.OrderByDescending(v => v.Type.TypeName),
                SortState.MileageAsc => vehicleDtos.OrderBy(v => v.Mileage),
                SortState.MileageDesc => vehicleDtos.OrderByDescending(v => v.Mileage),
                _ => vehicleDtos.OrderBy(v => v.Id)
            };

            var vehicles = _mapper.Map<IEnumerable<VehicleDto>, List<VehicleViewModel>>(vehicleDtos);
            return View(vehicles);
        }

        public async Task<IActionResult> VehicleDetails(int id)
        {
            var vehicleDto = await db.Get(id);
            var vehicle = _mapper.Map<VehicleDto, VehicleViewModel>(vehicleDto);

            return View(vehicle) ?? (IActionResult)NotFound();
        }

        public IActionResult CreateVehicle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateVehicle(VehicleViewModel vehicle)
        {
            if (ModelState.IsValid)
            {
                var vehicleDto = _mapper.Map<VehicleViewModel, VehicleDto>(vehicle);
                db.Create(vehicleDto);
                return RedirectToAction("ViewVehicles");
            }

            return RedirectToAction("CreateVehicle");
            
        }

        [HttpGet]
        [ActionName("DeleteVehicle")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                var vehicleDto = await db.Get(id);
                var vehicle = _mapper.Map<VehicleDto, VehicleViewModel>(vehicleDto);
                if (vehicle != null)
                    return View(vehicle);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteVehicle(int? id)
        {
            if (id != null)
            {
                var vehicleDto = await db.Get(id);
                if (vehicleDto != null)
                {
                    db.Delete(vehicleDto);
                    return RedirectToAction("ViewVehicles");
                }
            }
            return NotFound();
        }

        public async Task<IActionResult> EditVehicle(int? id)
        {
            if (id != null)
            {
                var vehicleDto = await db.Get(id);
                var vehicle = _mapper.Map<VehicleDto, VehicleViewModel>(vehicleDto);
                if (vehicle != null)
                    return View(vehicle);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult EditVehicle(VehicleViewModel vehicle)
        {
            if (ModelState.IsValid)
            {
                var vehicleDto = _mapper.Map<VehicleViewModel, VehicleDto>(vehicle);
                db.Update(vehicleDto);
                return RedirectToAction("ViewVehicles");
            }
            else
                return RedirectToAction("EditVehicle");
        }
    }
}
