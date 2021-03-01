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
    public class SparePartViewModelController : Controller
    {
        IService<SparePartDto> db;
        private readonly IMapper _mapper;

        public SparePartViewModelController(IService<SparePartDto> service, IMapper mapper)
        {
            db = service;
            _mapper = mapper;
        }

        public IActionResult ViewSpareParts()
        {
            var partsDtos = db.GetAll();
            var parts = _mapper.Map<IEnumerable<SparePartDto>, List<SparePartViewModel>>(partsDtos);
            return View(parts);
        }

        public IActionResult CreateSparePart()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateSparePart(SparePartViewModel part)
        {
            var partDto = _mapper.Map<SparePartViewModel, SparePartDto>(part);
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
                var part = _mapper.Map<SparePartDto, SparePartViewModel>(partDto);
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
                var part = _mapper.Map<SparePartDto, SparePartViewModel>(partDto);
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
                var partDto = _mapper.Map<SparePartViewModel, SparePartDto>(part);
                db.Update(partDto);
                return RedirectToAction("ViewSpareParts");
            }
            else
                return RedirectToAction("EditSparePart");
        }
    }
}
