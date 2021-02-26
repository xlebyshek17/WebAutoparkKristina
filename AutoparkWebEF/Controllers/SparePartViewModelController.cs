﻿using AutoMapper;
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
    public class SparePartViewModelController : Controller
    {
        IService<SparePartDto> db;

        public SparePartViewModelController(IService<SparePartDto> service)
        {
            db = service;
        }

        public async Task<IActionResult> ViewSpareParts()
        {
            var partsDtos = await db.GetAll();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SparePartDto, SparePartViewModel>()).CreateMapper();
            var parts = mapper.Map<IEnumerable<SparePartDto>, List<SparePartViewModel>>(partsDtos);
            return View(parts);
        }

        public IActionResult CreateSparePart()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateSparePart(SparePartViewModel part)
        {
            var partDto = new SparePartDto { Id = part.Id, Name = part.Name };
            db.Create(partDto);
            return RedirectToAction("ViewSpareParts");
        }

        [HttpGet]
        [ActionName("DeleteSparePart")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                var partDto = await db.Get(id);
                var part = new SparePartViewModel { Id = partDto.Id, Name = partDto.Name };
                if (part != null)
                    return View(part);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSparePart(int? id)
        {
            if (id != null)
            {
                var partDto = await db.Get(id);
                if (partDto != null)
                {
                    db.Delete(partDto);
                    return RedirectToAction("ViewSpareParts");
                }
            }
            return NotFound();
        }

        public async Task<IActionResult> EditSparePart(int? id)
        {
            if (id != null)
            {
                var partDto = await db.Get(id);
                var part = new SparePartViewModel { Id = partDto.Id, Name = partDto.Name };
                if (part != null)
                    return View(part);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult EditSparePart(SparePartViewModel part)
        {
            if (ModelState.IsValid)
            {
                var partDto = new SparePartDto { Id = part.Id, Name = part.Name };
                db.Update(partDto);
                return RedirectToAction("ViewSpareParts");
            }
            else
                return RedirectToAction("EditSparePart");
        }
    }
}
