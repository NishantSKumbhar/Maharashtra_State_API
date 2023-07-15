using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using PatanWalks.Data;
using PatanWalks.Models.Domain;
using PatanWalks.Models.DTO;
using PatanWalks.Repositories;
using System.Data.OleDb;

namespace PatanWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictController : ControllerBase
    {
        private readonly MaharashtraDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IDistrictRepository districtRepository;

        public DistrictController(MaharashtraDbContext dbContext, IMapper mapper, IDistrictRepository districtRepository)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.districtRepository = districtRepository;
        }

        [HttpGet]
        public async  Task<ActionResult<List<DistrictDTO>>> GetDistricts()
        {
            // Make DTO
            var districtDTO = new List<DistrictDTO>();

            var districts = await districtRepository.GetAllDistrictsAsync();


            // Map the DTO
            //foreach (var dis in districts)
            //{
            //    districtDTO.Add(new DistrictDTO()
            //    {
            //        Id = dis.Id,
            //        Name = dis.Name,
            //        Description = dis.Description,
            //        AreaInSqKm = dis.AreaInSqKm,
            //        DistrictImageUrl = dis.DistrictImageUrl,
            //        DivisionId = dis.DivisionId,
            //        PopulationId = dis.PopulationId
            //    });
            //} 

            // Return the DTO

            //return Ok(districtDTO);
            return Ok(mapper.Map<List<DistrictDTO>>(districts));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<DistrictDTO>> GetDistrictById([FromRoute]Guid id)
        {



            //var district = dbContext.Districts.Find(id);
            var district = await districtRepository.GetDistrictByIdAsync(id);
            // Above Find Method will take only primary key
            //var district = dbContext.Districts.FirstOrDefault(x => x.Id == id);


            if (district == null) 
            {
                return NotFound(new { Message = "District with that ID not Found."});
            }

            //var districtDTO = new DistrictDTO()
            //{
            //    Id = district.Id,
            //    Name = district.Name,
            //    Description = district.Description,
            //    AreaInSqKm = district.AreaInSqKm,
            //    DistrictImageUrl = district.DistrictImageUrl,
            //    DivisionId = district.DivisionId,
            //    PopulationId = district.PopulationId
            //};
            var districtDTO = mapper.Map<DistrictDTO>(district);
            return Ok(districtDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DistrictPostDTO addDistrictDTO) 
        {
            if (ModelState.IsValid)
            {
                //var districtModel = new District
                //{

                //    Name = addDistrictDTO.Name,
                //    Description = addDistrictDTO.Description,
                //    DistrictImageUrl = addDistrictDTO.DistrictImageUrl,
                //    AreaInSqKm = addDistrictDTO.AreaInSqKm,
                //    DivisionId = addDistrictDTO.DivisionId,
                //    PopulationId = addDistrictDTO.PopulationId
                //};
                var districtModel = mapper.Map<District>(addDistrictDTO);

                //await dbContext.Districts.AddAsync(districtModel);
                //await dbContext.SaveChangesAsync();
                var district = await districtRepository.PostDistrictAsync(districtModel);
                var districtDto = mapper.Map<DistrictDTO>(district);
                //var districtDto = new DistrictDTO
                //{
                //    Id = districtModel.Id,
                //    Name = districtModel.Name,
                //    Description = districtModel.Description,
                //    DistrictImageUrl = districtModel.DistrictImageUrl,
                //    AreaInSqKm = districtModel.AreaInSqKm,
                //    DivisionId = districtModel.DivisionId,
                //    PopulationId = districtModel.PopulationId
                //};

                return CreatedAtAction(nameof(GetDistrictById), new { id = districtDto.Id }, districtDto);
            }
            else
            {
                return BadRequest(ModelState);
            }
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateDistrictDTO updatedDistrict)
        {
            if (ModelState.IsValid)
            {
                var district = mapper.Map<District>(updatedDistrict);
                var districtModel = await districtRepository.UpdateDistrictAsync(id, district);

                if (districtModel == null)
                {
                    return NotFound();
                }

                //districtModel.Name = updatedDistrict.Name;
                //districtModel.Description = updatedDistrict.Description;
                //districtModel.AreaInSqKm = updatedDistrict.AreaInSqKm;
                //districtModel.DistrictImageUrl = updatedDistrict.DistrictImageUrl;
                //districtModel.DivisionId = updatedDistrict.DivisionId;
                //districtModel.PopulationId = updatedDistrict.PopulationId;

                //dbContext.SaveChanges();
                var DistrictDTO = mapper.Map<DistrictDTO>(districtModel);
                //var DistrictDTO = new DistrictDTO
                //{
                //    Id = districtModel.Id,
                //    Name = districtModel.Name,
                //    Description = districtModel.Description,
                //    DistrictImageUrl = districtModel.DistrictImageUrl,
                //    DivisionId = updatedDistrict.DivisionId,
                //    PopulationId = updatedDistrict.PopulationId,
                //    AreaInSqKm = districtModel.AreaInSqKm
                //};
                // Always pass DTO , Not Domain Model
                return Ok(DistrictDTO);
            }
            else
            {
                return BadRequest(ModelState);
            }
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var districtModel = await districtRepository.DeleteDistrictAsync(id);
            if(districtModel == null)
            {
                return NotFound();
            }

            //dbContext.Districts.Remove(districtModel);
            //dbContext.SaveChanges();

            // you can return deleted object
            return Ok();
        }
    }
}
