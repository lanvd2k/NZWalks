using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NZWalks.Web.Models;
using NZWalks.Web.Models.DTO;
using NZWalks.Web.Services.IServices;

namespace NZWalks.Web.Controllers
{
    public class RegionController : Controller
    {
        private readonly IRegionService _regionService;
        private readonly IMapper _mapper;
        public RegionController(IRegionService regionService, IMapper mapper)
        {
            _regionService = regionService;
            _mapper = mapper;
        }
        public async Task<IActionResult> IndexRegion()
        {
            List<RegionDTO> list = new();
            var respone = await _regionService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (respone != null && respone.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<RegionDTO>>(Convert.ToString(respone.Result));
            }
            
            return View(list);
        }

        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateRegion()
        {

            return View();
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRegion(AddRegionRequest model)
        {
            if (ModelState.IsValid)
            {
                var respone = await _regionService.AddAsync<APIResponse>(model, HttpContext.Session.GetString(SD.SessionToken));
                if (respone != null && respone.IsSuccess)
                {
                    TempData["success"] = "Villa created successfully";
                    return RedirectToAction(nameof(IndexRegion));
                }
            }
            TempData["error"] = "Error encountered";
            return View(model);
        }

        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateRegion(Guid regionId)
        {
            var respone = await _regionService.GetAsync<APIResponse>(regionId, HttpContext.Session.GetString(SD.SessionToken));
            if (respone != null && respone.IsSuccess)
            {

                RegionDTO model = JsonConvert.DeserializeObject<RegionDTO>(Convert.ToString(respone.Result));
                return View(_mapper.Map<UpdateRegionRequest>(model));
            }
            return NotFound();

        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateRegion(UpdateRegionRequest model)
        {
            if (ModelState.IsValid)
            {
                TempData["success"] = "Villa updated successfully";
                var respone = await _regionService.UpdateAsync<APIResponse>(model, HttpContext.Session.GetString(SD.SessionToken));
                if (respone != null && respone.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexRegion));
                }
            }
            TempData["error"] = "Error encountered";
            return View(model);
        }

        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteRegion(Guid regionId)
        {
            var respone = await _regionService.GetAsync<APIResponse>(regionId, HttpContext.Session.GetString(SD.SessionToken));
            if (respone != null && respone.IsSuccess)
            {
                RegionDTO model = JsonConvert.DeserializeObject<RegionDTO>(Convert.ToString(respone.Result));
                return View(model);
            }
            return NotFound();
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRegion(RegionDTO model)
        {
            var respone = await _regionService.DeleteAsync<APIResponse>(model.Id, HttpContext.Session.GetString(SD.SessionToken));
            if (respone != null && respone.IsSuccess)
            {
                TempData["success"] = "Villa deleted successfully";
                return RedirectToAction(nameof(IndexRegion));
            }
            TempData["error"] = "Error encountered";
            return View(model);
        }
    }
}
