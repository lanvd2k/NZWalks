using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Net;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class RegionsController : Controller
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;
        protected APIResponse _respone;
        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;
            _respone = new();
        }

        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetAllRegionsAsync()
        {
            var regions = await _regionRepository.GetAllAsync();

            //return regions DTO
            //var regionsDTO = _mapper.Map<List<RegionDTO>>(regions);
            _respone.Result = _mapper.Map<List<RegionDTO>>(regions);
            _respone.StatusCode = System.Net.HttpStatusCode.OK;

            return Ok(_respone);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        //[Authorize(Roles = "reader")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await _regionRepository.GetAsync(id);
            if(region == null)
            {
                return NotFound();
            }

            var regionDTO = _mapper.Map<RegionDTO>(region);

            return Ok(regionDTO);
        }

        [HttpPost]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> AddRegionAsync(AddRegionRequest addRegionRequest)
        {
            // Request to domain model
            var region = new Region()
            {
                Code = addRegionRequest.Code,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Name = addRegionRequest.Name,
                Population = addRegionRequest.Population,
                ImageUrl = addRegionRequest.ImageUrl
            };

            // Pass detail to repository
            region = await _regionRepository.AddAsync(region);

            // Convert back to DTO
            var regionDTO = new RegionDTO()
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population,
                ImageUrl = region.ImageUrl
            };

            return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDTO.Id }, regionDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> DeleteRegionAsync(Guid id)
        {
            
            // Get region from database
            var region = await _regionRepository.DeleteAsync(id);

            // If null NotFound
            if (region == null)
            {
                return NotFound();
            }
            region = await _regionRepository.DeleteAsync(id);
            // Convert response back to DTO

            var regionDTO = _mapper.Map<RegionDTO>(region);

            // Return OK response
            return Ok(regionDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> UpdateRegionAsync([FromRoute] Guid id, [FromBody] UpdateRegionRequest updateRegionRequest)
        {
            if (updateRegionRequest == null || updateRegionRequest.Id != id)
            {
                return NotFound();
            }
            // Convert DTO to domain model
            var region = new Region()
            {
                Code = updateRegionRequest.Code,
                Area = updateRegionRequest.Area,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Name = updateRegionRequest.Name,
                Population = updateRegionRequest.Population,
                ImageUrl = updateRegionRequest.ImageUrl
            };

            // Update Region using repository
            region = await _regionRepository.UpdateAsync(id, region);

            // If null then NotFound
            if (region == null)
            {
                return NotFound();
            }

            // Convert Domain back to DTO
            var regionDTO = _mapper.Map<RegionDTO>(region);

            //_respone.StatusCode = System.Net.HttpStatusCode.NoContent;
            //_respone.IsSuccess = true;

            // Return OK response
            return Ok(regionDTO);
        }
    }
}
