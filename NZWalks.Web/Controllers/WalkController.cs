using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using NZWalks.Web.Models;
using NZWalks.Web.Models.DTO;
using NZWalks.Web.Services.IServices;

namespace NZWalks.Web.Controllers
{
    public class WalkController : Controller
    {
        private readonly IWalkService _walkService;
        private readonly IRegionService _regionService;
        private readonly IWalkDifficultyService _walkDifficultyService;
        private readonly IMapper _mapper;
        public WalkController(IWalkService walkService, IMapper mapper, IRegionService regionService, IWalkDifficultyService walkDifficultyService)
        {
            _walkService = walkService;
            _mapper = mapper;
            _regionService = regionService;
            _walkDifficultyService = walkDifficultyService;
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

        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateWalk()
        {
            //them
            AddWalkRequestVM addWalkRequest = new();
            var responeRegion = await _regionService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (responeRegion != null && responeRegion.IsSuccess)
            {
                addWalkRequest.RegionList = JsonConvert
                    .DeserializeObject<List<RegionDTO>>(Convert.ToString(responeRegion.Result))
                    .Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
            }
            var responeWalkDifficulty = await _walkDifficultyService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (responeWalkDifficulty != null && responeWalkDifficulty.IsSuccess)
            {
                addWalkRequest.WalkDifficultyList = JsonConvert
                    .DeserializeObject<List<WalkDifficultyDTO>>(Convert.ToString(responeWalkDifficulty.Result))
                    .Select(i => new SelectListItem
                    {
                        Text = i.Code,
                        Value = i.Id.ToString()
                    });
            }
            //het them
            return View(addWalkRequest);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateWalk(AddWalkRequestVM model)
        {
            if (ModelState.IsValid)
            {
                var respone = await _walkService.AddAsync<APIResponse>(model.Walk, HttpContext.Session.GetString(SD.SessionToken));
                if (respone != null && respone.IsSuccess)
                {
                    TempData["success"] = "Walk created successfully";
                    return RedirectToAction(nameof(IndexWalk));
                }
            }

            //them
            var resRegion = await _regionService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (resRegion != null && resRegion.IsSuccess)
            {
                model.RegionList = JsonConvert
                    .DeserializeObject<List<RegionDTO>>(Convert.ToString(resRegion.Result))
                    .Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
            }

            var resWalkDifficulty = await _walkDifficultyService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (resWalkDifficulty != null && resWalkDifficulty.IsSuccess)
            {
                model.WalkDifficultyList = JsonConvert
                    .DeserializeObject<List<WalkDifficultyDTO>>(Convert.ToString(resWalkDifficulty.Result))
                    .Select(i => new SelectListItem
                    {
                        Text = i.Code,
                        Value = i.Id.ToString()
                    });
            }
            //het them

            //TempData["error"] = "Error encountered";
            return View(model);
        }

        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateWalk(Guid id)
        {
            //them
            UpdateWalkRequestVM updateWalkRequestVM = new();
            var responeRegion = await _regionService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (responeRegion != null && responeRegion.IsSuccess)
            {
                updateWalkRequestVM.RegionList = JsonConvert
                    .DeserializeObject<List<RegionDTO>>(Convert.ToString(responeRegion.Result))
                    .Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
            }
            var responeWalkDifficulty = await _walkDifficultyService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (responeWalkDifficulty != null && responeWalkDifficulty.IsSuccess)
            {
                updateWalkRequestVM.WalkDifficultyList = JsonConvert
                    .DeserializeObject<List<WalkDifficultyDTO>>(Convert.ToString(responeWalkDifficulty.Result))
                    .Select(i => new SelectListItem
                    {
                        Text = i.Code,
                        Value = i.Id.ToString()
                    });
            }
            //het them
            var respone = await _walkService.GetAsync<APIResponse>(id, HttpContext.Session.GetString(SD.SessionToken));
            if (respone != null && respone.IsSuccess)
            {

                WalkDTO model = JsonConvert.DeserializeObject<WalkDTO>(Convert.ToString(respone.Result));
                updateWalkRequestVM.Walk = _mapper.Map<UpdateWalkRequest>(model);
                return View(updateWalkRequestVM);
            }
            return NotFound();

        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateWalk(Guid id, UpdateWalkRequestVM model)
        {
            if (ModelState.IsValid)
            {
                model.Walk.Id = id;
                //them
                //UpdateWalkRequestVM updateWalkRequestVM = new();
                var responeRegion = await _regionService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
                if (responeRegion != null && responeRegion.IsSuccess)
                {
                    model.RegionList = JsonConvert
                        .DeserializeObject<List<RegionDTO>>(Convert.ToString(responeRegion.Result))
                        .Select(i => new SelectListItem
                        {
                            Text = i.Name,
                            Value = i.Id.ToString()
                        });
                }
                var responeWalkDifficulty = await _walkDifficultyService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
                if (responeWalkDifficulty != null && responeWalkDifficulty.IsSuccess)
                {
                    model.WalkDifficultyList = JsonConvert
                        .DeserializeObject<List<WalkDifficultyDTO>>(Convert.ToString(responeWalkDifficulty.Result))
                        .Select(i => new SelectListItem
                        {
                            Text = i.Code,
                            Value = i.Id.ToString()
                        });
                }
                //het them

                var respone = await _walkService.UpdateAsync<APIResponse>(model.Walk, HttpContext.Session.GetString(SD.SessionToken));
                if (respone != null && respone.IsSuccess)
                {
                    TempData["success"] = "Walk updated successfully";
                    return RedirectToAction(nameof(IndexWalk));
                }
            }
            TempData["error"] = "Error encountered";
            return View(model);
        }

        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteWalk(Guid id)
        {
            var respone = await _walkService.GetAsync<APIResponse>(id, HttpContext.Session.GetString(SD.SessionToken));
            if (respone != null && respone.IsSuccess)
            {
                WalkDTO model = JsonConvert.DeserializeObject<WalkDTO>(Convert.ToString(respone.Result));
                return View(model);
            }
            return NotFound();
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteWalk(Guid id, RegionDTO model)
        {
            model.Id = id;
            var respone = await _walkService.DeleteAsync<APIResponse>(model.Id, HttpContext.Session.GetString(SD.SessionToken));
            if (respone != null && respone.IsSuccess)
            {
                TempData["success"] = "Walk deleted successfully";
                return RedirectToAction(nameof(IndexWalk));
            }
            TempData["error"] = "Error encountered";
            return View(model);
        }
    }
}
