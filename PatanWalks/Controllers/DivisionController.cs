using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatanWalks.Data;
using PatanWalks.Models.Domain;
using PatanWalks.Models.DTO;
using PatanWalks.Repositories;

namespace PatanWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DivisionController : ControllerBase
    {
        private readonly MaharashtraDbContext maharashtraDbContext;
        private readonly IDivisionRepository divisionRepository;
        public DivisionController(MaharashtraDbContext maharashtraDbContex, IDivisionRepository divisionRepo)
        {
            this.maharashtraDbContext = maharashtraDbContext;
            this.divisionRepository = divisionRepo;
        }
        [HttpGet]
        public async Task<ActionResult<List<DivisionGetDTO>>> GetAllDivisions()  // change due to async
        {
            var DivisionDTO = new List<DivisionGetDTO>();

            //var Divisions = await maharashtraDbContext.Divisions.ToListAsync();// change due to async
            var Divisions = await divisionRepository.GetAllAsyncDivisions();
            foreach (var Division in Divisions) 
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
        public async  Task<ActionResult<DivisionGetDTO>> GetDivisionByID(Guid id) // change due to async
        {
            var division = await maharashtraDbContext.Divisions.FindAsync(id);// change due to async

            if (division == null)
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
        public async Task<ActionResult> PostDivision([FromBody] DivisionPostDTO newDivision)// change due to async
        {
            var divisionModel = new Division
            {
                Name = newDivision.Name,
                Code = newDivision.Code,
                DivisionImageUrl= newDivision.DivisionImageUrl
            };

            await maharashtraDbContext.Divisions.AddAsync(divisionModel);// change due to async
            await maharashtraDbContext.SaveChangesAsync();// change due to async

            var divisionDTO = new DivisionGetDTO()
            {
                Name = divisionModel.Name,
                Code = divisionModel.Code,
                DivisionImageUrl= divisionModel.DivisionImageUrl
            };

            return CreatedAtAction(nameof(GetDivisionByID), new { id = divisionModel.Id }, divisionDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DivisionGetDTO>> UpdateDivision([FromRoute] Guid id, [FromBody] DivisionPutDTO updatedDivision)// change due to async
        {
            var division = await maharashtraDbContext.Divisions.FirstOrDefaultAsync(x => x.Id == id);// change due to async
            if (division == null)
            {
                return NotFound();
            }
            division.Name = updatedDivision.Name;
            division.Code = updatedDivision.Code;
            division.DivisionImageUrl = updatedDivision.DivisionImageUrl;

            await maharashtraDbContext.SaveChangesAsync();// change due to async

            var divisionDTO = new DivisionGetDTO()
            {
                Name = division.Name,
                Code = division.Code,
                DivisionImageUrl= division.DivisionImageUrl
            };
            return Ok(divisionDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DivisionGetDTO>> DeleteDivision([FromRoute] Guid id)// change due to async
        {
            var division = await maharashtraDbContext.Divisions.FirstOrDefaultAsync(x => x.Id == id);// change due to async
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
            await maharashtraDbContext.SaveChangesAsync();// change due to async
            return Ok(divisionDTO);
        }

    }
}
