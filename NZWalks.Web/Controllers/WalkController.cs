using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NZWalks.Web.Models;
using NZWalks.Web.Models.DTO;
using NZWalks.Web.Services.IServices;

namespace NZWalks.Web.Controllers
{
    public class WalkController : Controller
    {
        private readonly IWalkService _walkService;
        private readonly IMapper _mapper;
        public WalkController(IWalkService walkService, IMapper mapper)
        {
            _walkService = walkService;
            _mapper = mapper;
        }
        public async Task<IActionResult> IndexWalk()
        {
            List<WalkDTO> list = new();
            var respone = await _walkService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (respone != null && respone.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<WalkDTO>>(Convert.ToString(respone.Result));
            }
            
            return View(list);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateWalk()
        {

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateWalk(AddWalkRequest model)
        {
            if (ModelState.IsValid)
            {
                var respone = await _walkService.AddAsync<APIResponse>(model, HttpContext.Session.GetString(SD.SessionToken));
                if (respone != null && respone.IsSuccess)
                {
                    TempData["success"] = "Villa created successfully";
                    return RedirectToAction(nameof(IndexWalk));
                }
            }
            TempData["error"] = "Error encountered";
            return View(model);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateWalk(Guid walkId)
        {
            var respone = await _walkService.GetAsync<APIResponse>(walkId, HttpContext.Session.GetString(SD.SessionToken));
            if (respone != null && respone.IsSuccess)
            {

                WalkDTO model = JsonConvert.DeserializeObject<WalkDTO>(Convert.ToString(respone.Result));
                return View(_mapper.Map<UpdateWalkRequest>(model));
            }
            return NotFound();

        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateWalk(UpdateWalkRequest model)
        {
            if (ModelState.IsValid)
            {
                TempData["success"] = "Villa updated successfully";
                var respone = await _walkService.UpdateAsync<APIResponse>(model, HttpContext.Session.GetString(SD.SessionToken));
                if (respone != null && respone.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexWalk));
                }
            }
            TempData["error"] = "Error encountered";
            return View(model);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteWalk(Guid walkId)
        {
            var respone = await _walkService.GetAsync<APIResponse>(walkId, HttpContext.Session.GetString(SD.SessionToken));
            if (respone != null && respone.IsSuccess)
            {
                WalkDTO model = JsonConvert.DeserializeObject<WalkDTO>(Convert.ToString(respone.Result));
                return View(model);
            }
            return NotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteWalk(RegionDTO model)
        {
            var respone = await _walkService.DeleteAsync<APIResponse>(model.Id, HttpContext.Session.GetString(SD.SessionToken));
            if (respone != null && respone.IsSuccess)
            {
                TempData["success"] = "Villa deleted successfully";
                return RedirectToAction(nameof(IndexWalk));
            }
            TempData["error"] = "Error encountered";
            return View(model);
        }
    }
}
