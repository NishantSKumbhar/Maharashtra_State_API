using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using PatanWalks.Data;
using PatanWalks.Models.Domain;
using PatanWalks.Models.DTO;

namespace PatanWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictController : ControllerBase
    {
        private readonly MaharashtraDbContext dbContext;
        public DistrictController(MaharashtraDbContext dbContext)
        {
             this.dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<District>> GetDistricts()
        {
            // Make DTO
            var districtDTO = new List<DistrictDTO>();

            var districts = dbContext.Districts.ToList();


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
        public IActionResult Create([FromBody] DistrictDTO addDistrictDTO) 
        {
            var districtModel = new District
            {
                
                Name = addDistrictDTO.Name,
                Description = addDistrictDTO.Description,
                DistrictImageUrl = addDistrictDTO.DistrictImageUrl,
                AreaInSqKm = addDistrictDTO.AreaInSqKm,
                DivisionId = addDistrictDTO.DivisionId,
                PopulationId = addDistrictDTO.PopulationId
            };

            dbContext.Districts.Add(districtModel);
            dbContext.SaveChanges();

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
    }
}
