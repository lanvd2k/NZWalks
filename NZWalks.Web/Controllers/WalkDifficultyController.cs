using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NZWalks.Web.Models;
using NZWalks.Web.Models.DTO;
using NZWalks.Web.Services;
using NZWalks.Web.Services.IServices;

namespace NZWalks.Web.Controllers
{
    public class WalkDifficultyController : Controller
    {
        private readonly IWalkDifficultyService _walkDifficultyService;
        private readonly IMapper _mapper;
        public WalkDifficultyController(IWalkDifficultyService walkDifficultyService, IMapper mapper)
        {
            _walkDifficultyService = walkDifficultyService;
            _mapper = mapper;
        }
        public async Task<IActionResult> IndexWalkDifficulty()
        {
            List<WalkDifficultyDTO> list = new();
            var respone = await _walkDifficultyService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (respone != null && respone.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<WalkDifficultyDTO>>(Convert.ToString(respone.Result));
            }

            return View(list);
        }

        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateWalkDifficulty()
        {

            return View();
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateWalkDifficulty(AddWalkDifficultyRequest model)
        {
            if (ModelState.IsValid)
            {
                var respone = await _walkDifficultyService.AddAsync<APIResponse>(model, HttpContext.Session.GetString(SD.SessionToken));
                if (respone != null && respone.IsSuccess)
                {
                    TempData["success"] = "Villa created successfully";
                    return RedirectToAction(nameof(IndexWalkDifficulty));
                }
            }
            TempData["error"] = "Error encountered";
            return View(model);
        }

        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateWalkDifficulty(Guid walkDifficultyId)
        {
            var respone = await _walkDifficultyService.GetAsync<APIResponse>(walkDifficultyId, HttpContext.Session.GetString(SD.SessionToken));
            if (respone != null && respone.IsSuccess)
            {

                WalkDifficultyDTO model = JsonConvert.DeserializeObject<WalkDifficultyDTO>(Convert.ToString(respone.Result));
                return View(_mapper.Map<UpdateWalkDifficultyRequest>(model));

            }
            return NotFound();

        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateWalkDifficulty(UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            if (ModelState.IsValid)
            {
                TempData["success"] = "Villa updated successfully";
                var respone = await _walkDifficultyService.UpdateAsync<APIResponse>(updateWalkDifficultyRequest, HttpContext.Session.GetString(SD.SessionToken));
                if (respone != null && respone.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexWalkDifficulty));
                }
            }
            TempData["error"] = "Error encountered";
            return View(updateWalkDifficultyRequest);
        }

        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteWalkDifficulty(Guid walkDifficultyId)
        {
            var respone = await _walkDifficultyService.GetAsync<APIResponse>(walkDifficultyId, HttpContext.Session.GetString(SD.SessionToken));
            if (respone != null && respone.IsSuccess)
            {
                WalkDifficultyDTO model = JsonConvert.DeserializeObject<WalkDifficultyDTO>(Convert.ToString(respone.Result));
                return View(model);
            }
            return NotFound();
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteWalkDifficulty(WalkDifficultyDTO model)
        {
            var respone = await _walkDifficultyService.DeleteAsync<APIResponse>(model.Id, HttpContext.Session.GetString(SD.SessionToken));
            if (respone != null && respone.IsSuccess)
            {
                TempData["success"] = "Villa deleted successfully";
                return RedirectToAction(nameof(IndexWalkDifficulty));
            }
            TempData["error"] = "Error encountered";
            return View(model);
        }
    }
}
