using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    public class WalkDifficultyController : Controller
    {
        private readonly IWalkDifficultyRepository _walkDifficultyRepository;
        private readonly IMapper _mapper;
        protected APIResponse _respone;
        public WalkDifficultyController(IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            _walkDifficultyRepository = walkDifficultyRepository;
            _mapper = mapper;
            _respone = new();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWalkDifficultiesAsync()
        {
            var walkDifficultyDomain = await _walkDifficultyRepository.GetAllAsync();
            //var walkDifficultyDTO = _mapper.Map<List<WalkDifficultyDTO>>(walkDifficultyDomain);
            _respone.Result = _mapper.Map<List<WalkDifficultyDTO>>(walkDifficultyDomain);
            _respone.StatusCode = System.Net.HttpStatusCode.OK;
            return Ok(_respone);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDifficultyById")]
        public async Task<IActionResult> GetWalkDifficultyById(Guid id)
        {
            var walkDifficulty = await _walkDifficultyRepository.GetAsync(id);
            if(walkDifficulty == null)
            {
                return NotFound();
            }
            var walkDifficultyDTO = _mapper.Map<WalkDifficultyDTO>(walkDifficulty);
            if(walkDifficultyDTO == null)
            {
                return NotFound();
            }
            return Ok(walkDifficultyDTO);
        }

        [HttpPost]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> AddWalkDifficultyAsync(AddWalkDifficultyRequest addWalkDifficultyRequest)
        {
            var walkDifficultyDomain = new WalkDifficulty()
            {
                Code = addWalkDifficultyRequest.Code
            };

            walkDifficultyDomain = await _walkDifficultyRepository.AddAsync(walkDifficultyDomain);

            var walkDifficultyDTO = _mapper.Map<WalkDifficultyDTO>(walkDifficultyDomain);

            return CreatedAtAction(nameof(GetWalkDifficultyById), new { id = walkDifficultyDTO.Id }, walkDifficultyDTO);
        }

        [HttpPut]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateWalkDifficultyAsync(Guid id, UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            var walkDifficultyDomain = new WalkDifficulty()
            {
                Code = updateWalkDifficultyRequest.Code
            };
            walkDifficultyDomain = await _walkDifficultyRepository.UpdateAsync(id, walkDifficultyDomain);
            if(walkDifficultyDomain == null)
            {
                return NotFound();
            }
            var walkDifficultyDTO = _mapper.Map<WalkDifficultyDTO>(walkDifficultyDomain);
            return Ok(walkDifficultyDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteWalkDifficultyAsync(Guid id)
        {
            var walkDifficultyDomain = await _walkDifficultyRepository.DeleteAsync(id);
            if (walkDifficultyDomain == null)
            {
                return NotFound();
            }
            var walkDifficultyDTO = _mapper.Map<WalkDifficultyDTO>(walkDifficultyDomain);
            return Ok(walkDifficultyDTO);
        }
    }
}
