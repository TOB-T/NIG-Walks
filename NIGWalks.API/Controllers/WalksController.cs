using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NIGWalks.API.CustomActionFilters;
using NIGWalks.API.Models.Domain;
using NIGWalks.API.Models.DTO;
using NIGWalks.API.Repositories;

namespace NIGWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalkRepository _walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this._mapper = mapper;
            this._walkRepository = walkRepository;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalksRequestDto addWalksRequestDto)
        {

            //Map Dto to domain model
            var walkDomain = _mapper.Map<Walk>(addWalksRequestDto);
            await _walkRepository.CreateAsync(walkDomain);

            //Map domain model back to dto
            var walkdto = _mapper.Map<WalkDto>(walkDomain);

            return Ok(walkdto);


        }

        [HttpGet]

        // Get: ?Api/Walks?filterOn=Name&FilterQuery=Track&sortBy=Name&isAscending=true&pageNumber=1&pageSize=10
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? IsAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var walkDomain = await _walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, IsAscending ?? true, pageNumber, pageSize);

            var walkDto = _mapper.Map<List<WalkDto>>(walkDomain);

            return Ok(walkDto);
        }

        [HttpGet]
        [Route("{Id}:Guid")]
        public async Task<IActionResult> GetbyId([FromRoute] Guid Id)
        {
           var domainModel = await _walkRepository.GetByIdAsync(Id);
            if (domainModel == null) 
            {
                return NotFound();
            }

            var dto = _mapper.Map<WalkDto>(domainModel);
            return Ok(dto);
        }

        [HttpPut]
        [Route("{Id}:Guid")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid Id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto) 
        {
            
                //Map Dto  to domain model
                var domainModel = _mapper.Map<Walk>(updateWalkRequestDto);

                domainModel = await _walkRepository.UpdateAsync(Id, domainModel);

                if (domainModel == null)
                {
                    return NotFound();
                }

                //map domain model back to dto

                var dto = _mapper.Map<WalkDto>(domainModel);

                return Ok(dto);
          
           
        }

        [HttpDelete]
        [Route("{Id}:Guid")]

        public async Task<IActionResult> Delete([FromRoute] Guid Id)
        {
            var domainModel =await _walkRepository.DeleteAsync(Id);
            if(domainModel == null)
            {
                return NotFound();
            }

            //map domain model back to dto

            var dto = _mapper.Map<WalkDto>(domainModel);
            return Ok(dto);
        }
    }
}
