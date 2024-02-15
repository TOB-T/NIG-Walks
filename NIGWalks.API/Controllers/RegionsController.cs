using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NIGWalks.API.CustomActionFilters;
using NIGWalks.API.Data;
using NIGWalks.API.Models.Domain;
using NIGWalks.API.Models.DTO;
using NIGWalks.API.Repositories;
using System.Text.Json;

namespace NIGWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class RegionsController : ControllerBase
    {
        private readonly NIGWalksDbContext _dbContext;
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RegionsController> _logger;

        public RegionsController(NIGWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper, ILogger<RegionsController> logger)
        {
            this._dbContext = dbContext;
            this._regionRepository = regionRepository;
            this._mapper = mapper;
            this._logger = logger;
        }

        
        [HttpGet]
        //[Authorize(Roles = "Reader")]
        public async  Task<IActionResult> GetAll()
        {
            try
            {
                //throw new Exception("This is a Custom Exception");
                _logger.LogInformation("GetAllRegions Action Method was Invoked");

                // Get Data from Database - Domain models
                //var regionsDomain = _dbContext.Regions.ToList();


                var regionsDomain = await _regionRepository.GetAllAsync();


                //Map Domain models To DTOs
                //var regionDTo = new List<RegionDto>();
                //foreach (var regionDomain in regionsDomain)
                //{
                //    regionDTo.Add(new RegionDto()
                //    {
                //        Id = regionDomain.Id,
                //        Code = regionDomain.Code,
                //        Name = regionDomain.Name,
                //        RegionImageUrl = regionDomain.RegionImageUrl,

                //    });
                //}

                // Map Domain models to Dto

                _logger.LogInformation($"Finished GetAllRegions request with data: {JsonSerializer.Serialize(regionsDomain)}");
                var regionDTo = _mapper.Map<List<RegionDto>>(regionsDomain);

                // return Dtos back to client
                return Ok(regionDTo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }

            


        }

        
        [HttpGet]
        [Route("{Id:Guid}")]
       // [Authorize(Roles = "Reader")]

        public async Task<IActionResult> GetbyId(Guid Id)
        {
            var regionDomain = await _regionRepository.GetByIdAsync(Id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            //Map/convert Region DOmain Model to Region DTO
            //var regionDto = new RegionDto()
            //{
            //    Id = regionDomain.Id,
            //    Code = regionDomain.Code,
            //    Name = regionDomain.Name,
            //    RegionImageUrl = regionDomain.RegionImageUrl,
            //};

            //Map Domain models to Region dTo
             var regionDto = _mapper.Map<RegionDto>(regionDomain);

            // return Dtos back to client
            return Ok(regionDto);

        }

        [HttpPost]
        [ValidateModelAttribute]
      //  [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {

            
                // Map or Convert DTO to Domain Model
                //var regionDomainModel = new Region()
                //{
                //    Code = addRegionRequestDto.Code,
                //    Name = addRegionRequestDto.Name,
                //    RegionImageUrl = addRegionRequestDto.RegionImageUrl,

                //};

                // Map or Convert DTO to Domain Model
                var regionDomainModel = _mapper.Map<Region>(addRegionRequestDto);

                //Use Domain Model to create Region
                //await _dbContext.Regions.AddAsync(regionDomainModel);
                //await _dbContext.SaveChangesAsync();

                regionDomainModel = await _regionRepository.CreateAsync(regionDomainModel);


                //Map Domain Model back to DTO

                //var regionDto = new RegionDto()
                //{
                //    Id = regionDomainModel.Id,
                //    Code = regionDomainModel.Code,
                //    Name = regionDomainModel.Name,
                //    RegionImageUrl = regionDomainModel.RegionImageUrl,

                //};

                //Map Domain Model back to DTO
                var regionDto = _mapper.Map<RegionDto>(regionDomainModel);


                return CreatedAtAction(nameof(GetbyId), new { Id = regionDto.Id }, regionDto);
          
            

        }

        // Update Region
        [HttpPut]
        [Route("{Id:Guid}")]
        [ValidateModel]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid Id, [FromBody] UpdateRegionRequestsDto updateRegionRequestsDto)
        {

            
                // Map DTO to Domain Model

                //var regionDomainModel = new Region()
                //{
                //    Code= updateRegionRequestsDto.Code,
                //    Name = updateRegionRequestsDto.Name,
                //    RegionImageUrl= updateRegionRequestsDto.RegionImageUrl,
                //};

                // Map DTO to Domain Model
                var regionDomainModel = _mapper.Map<Region>(updateRegionRequestsDto);




                // Check if region exists
                regionDomainModel = await _regionRepository.UpdateAsync(Id, regionDomainModel);
                if (regionDomainModel == null)
                {
                    return NotFound();
                }

                // Map Dto to Domain Model
                // regionDomainModel.Code = updateRegionRequestsDto.Code;
                // regionDomainModel.Name = updateRegionRequestsDto.Name;
                // regionDomainModel.RegionImageUrl = updateRegionRequestsDto.RegionImageUrl;



                //convert Domain Model to DTO
                //var regionDto = new RegionDto
                //{
                //    Id = regionDomainModel.Id,
                //    Code = regionDomainModel.Code,
                //    Name = regionDomainModel.Name,
                //    RegionImageUrl = regionDomainModel.RegionImageUrl,

                //};

                //convert Domain Model to DTO
                var regionDto = _mapper.Map<RegionDto>(regionDomainModel);

                return Ok(regionDto);

            
           




        }

        // Delete Region
        [HttpDelete]
        [Route("{Id:Guid}")]
       // [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid Id)
        {
            // var regionsDomainModel = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == Id);
            // if(regionsDomainModel == null)
            // {
            //     return NotFound();
            // }
            //_dbContext.Regions.Remove(regionsDomainModel);
            //await _dbContext.SaveChangesAsync();

            // return Ok();

            var regionsDomainModel = await _regionRepository.DeleteAsync(Id);
            if (regionsDomainModel == null)
            {
                return NotFound();
            }

           var regionDto = _mapper.Map<RegionDto>(regionsDomainModel);
            return Ok(regionDto);



        }
    }

}
