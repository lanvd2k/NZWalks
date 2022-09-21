using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Data;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalksController : Controller
    {
        private readonly IWalkRepository _walkRepository;
        private readonly IMapper _mapper;
        protected APIResponse _respone;
        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            _walkRepository = walkRepository;
            _mapper = mapper;
            _respone = new();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalksAsync()
        {
            // Fetch data from database
            var walks = await _walkRepository.GetAllAsync();

            // Convert domain walks to DTO walks
            //var walksDTO = _mapper.Map<List<WalkDTO>>(walks);
            _respone.Result = _mapper.Map<List<WalkDTO>>(walks);
            _respone.StatusCode = System.Net.HttpStatusCode.OK;

            // Return response
            return Ok(_respone);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkAsync")]
        public async Task<ActionResult<APIResponse>> GetWalkAsync(Guid id)
        {
            // Get Walk domain object from database
            var walkDomain = await _walkRepository.GetAsync(id);

            if (walkDomain != null)
            {
                _respone.IsSuccess = true;
                _respone.StatusCode = System.Net.HttpStatusCode.OK;
                _respone.Result = walkDomain;
            }
            if (walkDomain == null)
            {
                return NotFound();
            }

            // Convert domain object to DTO
            var walkDTO = _mapper.Map<WalkDTO>(walkDomain);

            // Return response
            return Ok(_respone);
        }

        [HttpPost]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> AddWalkAsync([FromBody] AddWalkRequest addWalkRequest)
        {
            // Convert DTO to Domain object
            var walkDomain = new Walk()
            {
                Length = addWalkRequest.Length,
                Name = addWalkRequest.Name,
                RegionId = addWalkRequest.RegionId,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId
            };

            // Pass domain object to Repository to persist this
            walkDomain = await _walkRepository.AddAsync(walkDomain);

            // Convert the domain object back to the DTO
            var walkDTO = _mapper.Map<WalkDTO>(walkDomain);

            // Send DTO response back to the client
            return CreatedAtAction(nameof(GetWalkAsync), new { id = walkDTO.Id }, walkDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> UpdateWalkAsync([FromRoute] Guid id, [FromBody] UpdateWalkRequest updateWalkRequest)
        {
            if (updateWalkRequest == null || updateWalkRequest.Id != id)
            {
                return NotFound();
            }
            // Convert DTO to Domain object
            var walkDomain = new Walk()
            {
                Length = updateWalkRequest.Length,
                Name = updateWalkRequest.Name,
                RegionId = updateWalkRequest.RegionId,
                WalkDifficultyId = updateWalkRequest.WalkDifficultyId
            };

            // Pass details to Repository - Get domain object in response (not null)
            walkDomain = await _walkRepository.UpdateAsync(id, walkDomain);

            // Handle null
            if(walkDomain == null)
            {
                return NotFound();
            }

            // Convert back to DTO
            var walkDTO = _mapper.Map<WalkDTO>(walkDomain);
            _respone.StatusCode = System.Net.HttpStatusCode.NoContent;
            _respone.IsSuccess = true;
            _respone.Result = walkDTO;

            // Return response
            return Ok(_respone);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> DeleteWalkAsync(Guid id)
        {
            // Call Repository to delele walk
            var walkDomain = await _walkRepository.DeleteAsync(id);

            if (walkDomain == null)
            {
                return NotFound();
            }

            var walkDTO = _mapper.Map<WalkDTO>(walkDomain);
            _respone.StatusCode = System.Net.HttpStatusCode.NoContent;
            _respone.IsSuccess = true;
            _respone.Result = walkDTO;

            return Ok(_respone);
        }
    }
}
