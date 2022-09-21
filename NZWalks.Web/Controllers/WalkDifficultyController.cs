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
                    TempData["success"] = "WalkDifficulty created successfully";
                    return RedirectToAction(nameof(IndexWalkDifficulty));
                }
            }
            TempData["error"] = "Error encountered";
            return View(model);
        }

        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateWalkDifficulty(Guid id)
        {
            var respone = await _walkDifficultyService.GetAsync<APIResponse>(id, HttpContext.Session.GetString(SD.SessionToken));
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
        public async Task<IActionResult> UpdateWalkDifficulty(Guid id, UpdateWalkDifficultyRequest model)
        {
            if (ModelState.IsValid)
            {
                model.Id = id;
                
                var respone = await _walkDifficultyService.UpdateAsync<APIResponse>(model, HttpContext.Session.GetString(SD.SessionToken));
                if (respone != null && respone.IsSuccess)
                {
                    TempData["success"] = "WalkDifficulty updated successfully";
                    return RedirectToAction(nameof(IndexWalkDifficulty));
                }
            }
            TempData["error"] = "Error encountered";
            return View(model);
        }

        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteWalkDifficulty(Guid id)
        {
            var respone = await _walkDifficultyService.GetAsync<APIResponse>(id, HttpContext.Session.GetString(SD.SessionToken));
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
        public async Task<IActionResult> DeleteWalkDifficulty(Guid id, WalkDifficultyDTO model)
        {
            model.Id = id;
            var respone = await _walkDifficultyService.DeleteAsync<APIResponse>(model.Id, HttpContext.Session.GetString(SD.SessionToken));
            if (respone != null && respone.IsSuccess)
            {
                TempData["success"] = "WalkDifficulty deleted successfully";
                return RedirectToAction(nameof(IndexWalkDifficulty));
            }
            TempData["error"] = "Error encountered";
            return View(model);
        }
    }
}
