using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatanWalks.Data;
using PatanWalks.Models.Domain;
using PatanWalks.Models.DTO;

namespace PatanWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DivisionController : ControllerBase
    {
        private readonly MaharashtraDbContext maharashtraDbContext;
        public DivisionController(MaharashtraDbContext maharashtraDbContext)
        {
            this.maharashtraDbContext = maharashtraDbContext;
        }
        [HttpGet]
        public ActionResult<List<DivisionGetDTO>> GetAllDivisions()
        {
            var DivisionDTO = new List<DivisionGetDTO>();

            var Divisions = maharashtraDbContext.Divisions.ToList();

            foreach(var Division in Divisions) 
            {
                DivisionDTO.Add(new DivisionGetDTO
                {
                    Name= Division.Name,
                    Code = Division.Code,
                    DivisionImageUrl= Division.DivisionImageUrl
                });
            }

            return Ok(DivisionDTO);
        }

        [HttpGet("{id}")]
        public ActionResult<DivisionGetDTO> GetDivisionByID(Guid id) 
        {
            var division = maharashtraDbContext.Divisions.Find(id);

            if(division == null)
            {
                return NotFound(new { Message = "ID NOT FOUND FOR PARTICULAR DIVISION" });
            }

            var divisionDTO = new DivisionGetDTO()
            {
                Name = division.Name,
                Code = division.Code,
                DivisionImageUrl= division.DivisionImageUrl
            };
            return Ok(divisionDTO);
        }

        [HttpPost]
        public ActionResult PostDivision([FromBody] DivisionPostDTO newDivision)
        {
            var divisionModel = new Division
            {
                Name = newDivision.Name,
                Code = newDivision.Code,
                DivisionImageUrl= newDivision.DivisionImageUrl
            };

            maharashtraDbContext.Divisions.Add(divisionModel);
            maharashtraDbContext.SaveChanges();

            var divisionDTO = new DivisionGetDTO()
            {
                Name = divisionModel.Name,
                Code = divisionModel.Code,
                DivisionImageUrl= divisionModel.DivisionImageUrl
            };

            return CreatedAtAction(nameof(GetDivisionByID), new { id = divisionModel.Id }, divisionDTO);
        }

        [HttpPut("{id}")]
        public ActionResult<DivisionGetDTO> UpdateDivision([FromRoute] Guid id, [FromBody] DivisionPutDTO updatedDivision)
        {
            var division = maharashtraDbContext.Divisions.FirstOrDefault(x => x.Id == id);
            if(division == null)
            {
                return NotFound();
            }
            division.Name = updatedDivision.Name;
            division.Code = updatedDivision.Code;
            division.DivisionImageUrl = updatedDivision.DivisionImageUrl;

            maharashtraDbContext.SaveChanges();

            var divisionDTO = new DivisionGetDTO()
            {
                Name = division.Name,
                Code = division.Code,
                DivisionImageUrl= division.DivisionImageUrl
            };
            return Ok(divisionDTO);
        }

        [HttpDelete("{id}")]
        public ActionResult<DivisionGetDTO> DeleteDivision([FromRoute] Guid id)
        {
            var division = maharashtraDbContext.Divisions.FirstOrDefault(x => x.Id == id);
            if (division == null)
            {
                return NotFound();
            }
            var divisionDTO = new DivisionGetDTO()
            {
                Name = division.Name,
                Code = division.Code,
                DivisionImageUrl= division.DivisionImageUrl
            };
            maharashtraDbContext.Divisions.Remove(division);
            maharashtraDbContext.SaveChanges();
            return Ok(divisionDTO);
        }

    }
}
