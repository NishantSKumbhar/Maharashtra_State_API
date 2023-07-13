using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using PatanWalks.Data;
using PatanWalks.Models.Domain;
using PatanWalks.Models.DTO;
using System.Data.OleDb;

namespace PatanWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictController : ControllerBase
    {
        private readonly MaharashtraDbContext dbContext;
        private readonly IMapper mapper;
        public DistrictController(MaharashtraDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        [HttpGet]
        public async  Task<ActionResult<IEnumerable<District>>> GetDistricts()
        {
            // Make DTO
            var districtDTO = new List<DistrictDTO>();

            var districts = await dbContext.Districts.ToListAsync();


            // Map the DTO
            foreach (var dis in districts)
            {
                districtDTO.Add(new DistrictDTO()
                {
                    Id = dis.Id,
                    Name = dis.Name,
                    Description = dis.Description,
                    AreaInSqKm = dis.AreaInSqKm,
                    DistrictImageUrl = dis.DistrictImageUrl,
                    DivisionId = dis.DivisionId,
                    PopulationId = dis.PopulationId
                });
            }

            // Return the DTO

            return Ok(districtDTO);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<DistrictDTO> GetDistrictById([FromRoute]Guid id)
        {
           


            var district = dbContext.Districts.Find(id);

            // Above Find Method will take only primary key
            //var district = dbContext.Districts.FirstOrDefault(x => x.Id == id);

            
            if (district == null) 
            {
                return NotFound(new { Message = "District with that ID not Found."});
            }

            var districtDTO = new DistrictDTO()
            {
                Id = district.Id,
                Name = district.Name,
                Description = district.Description,
                AreaInSqKm = district.AreaInSqKm,
                DistrictImageUrl = district.DistrictImageUrl,
                DivisionId = district.DivisionId,
                PopulationId = district.PopulationId
            };

            return Ok(districtDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DistrictPostDTO addDistrictDTO) 
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

            await dbContext.Districts.AddAsync(districtModel);
            await dbContext.SaveChangesAsync();

            var districtDto = new DistrictDTO
            {
                Id = districtModel.Id,
                Name = districtModel.Name,
                Description = districtModel.Description,
                DistrictImageUrl = districtModel.DistrictImageUrl,
                AreaInSqKm = districtModel.AreaInSqKm,
                DivisionId = districtModel.DivisionId,
                PopulationId = districtModel.PopulationId
            };

            return CreatedAtAction(nameof(GetDistrictById), new { id = districtDto.Id }, districtDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateDistrictDTO updatedDistrict)
        {
            var districtModel = dbContext.Districts.FirstOrDefault(x => x.Id == id);

            if(districtModel == null)
            {
                return NotFound();
            }

            districtModel.Name = updatedDistrict.Name;
            districtModel.Description = updatedDistrict.Description;
            districtModel.AreaInSqKm = updatedDistrict.AreaInSqKm;
            districtModel.DistrictImageUrl = updatedDistrict.DistrictImageUrl;
            districtModel.DivisionId = updatedDistrict.DivisionId;
            districtModel.PopulationId = updatedDistrict.PopulationId;

            dbContext.SaveChanges();

            var DistrictDTO = new DistrictDTO
            {
                Id = districtModel.Id,
                Name = districtModel.Name,
                Description = districtModel.Description,
                DistrictImageUrl = districtModel.DistrictImageUrl,
                DivisionId = updatedDistrict.DivisionId,
                PopulationId = updatedDistrict.PopulationId,
                AreaInSqKm = districtModel.AreaInSqKm
            };
            // Always pass DTO , Not Domain Model
            return Ok(DistrictDTO);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var districtModel = dbContext.Districts.FirstOrDefault(x => x.Id == id);
            if(districtModel == null)
            {
                return NotFound();
            }

            dbContext.Districts.Remove(districtModel);
            dbContext.SaveChanges();

            // you can return deleted object
            return Ok();
        }
    }
}
