using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NZWalks.Web.Models;
using NZWalks.Web.Models.DTO;
using NZWalks.Web.Services.IServices;
using System.Diagnostics;

namespace NZWalks.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRegionService _regionService;
        private readonly IMapper _mapper;
        public HomeController(IRegionService regionService, IMapper mapper)
        {
            _regionService = regionService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            List<RegionDTO> list = new();
            var respone = await _regionService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (respone != null && respone.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<RegionDTO>>(Convert.ToString(respone.Result));
            }

            return View(list);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}